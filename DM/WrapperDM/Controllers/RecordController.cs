using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using WrapperDM.Helpers;
using WrapperDM.Models;

namespace WrapperDM.Controllers;

public class RecordController
{
    private MemoryCache<Dictionary<long, RecordModel>> _recordCache;
    private const string Path = "api/record";
    private const string RecordKey = "RecordKey";

    public RecordController()
    {
        _recordCache = new MemoryCache<Dictionary<long, RecordModel>>();
    }

    public async Task<List<RecordModel>> GetAllRecords(string token)
    {
        // получаем из кэша
        if (!OfflineHelper.CheckForInternetConnection())
        {
            var records = _recordCache.GetSection(RecordKey).Values.ToList(); // лист объектов

            return records;
        }
        
        var resClient = new RestClient();
        var requestPath = "UrlHelper.DmApiUrl + Path";
        var request = new RestRequest(requestPath);
        request.AddHeader("Authentication", $"Bearer {token!}");
        request.Method = Method.Get;

        try
        {
            var response = await resClient.ExecuteAsync(request);
            var deserializedResponse = JsonConvert.DeserializeObject<List<RecordModel>>(response.Content);

            // добавляем в кэш
            var recordDictionary = new Dictionary<long, RecordModel>();

            foreach (var recordModel in deserializedResponse) // добавляем в словарь ключ/значение
            {
                var modelForCaching = new RecordModel();
                modelForCaching.Id = recordModel.Id;
                modelForCaching.Name = recordModel.Name;
                modelForCaching.Fields = recordModel.Fields;
                modelForCaching.ProjectId = recordModel.ProjectId;

                recordDictionary.Add(modelForCaching.Id, modelForCaching);
            }
         
            _recordCache.GetOrCreate(RecordKey, () => recordDictionary);
            return deserializedResponse;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
   
    public async Task<RecordModel> GetRecordById(long recordId, string token)
    {
        // получаем из кэша, если было получено в GetAll
        if (!OfflineHelper.CheckForInternetConnection()) // TODO: добавить проверку токена
        {
            var project = _recordCache.GetSection(RecordKey)
                .Where(x => x.Value.Id == recordId)
                .Select(x => x.Value).FirstOrDefault();
            return project;
        }

        var resClient = new RestClient();
        var requestPath = UrlHelper.DmApiUrl + Path + $"/{recordId}";
        var request = new RestRequest(requestPath);
        request.AddHeader("projectId", recordId);
        request.AddHeader("Authentication", $"Bearer {token!}");
        request.Method = Method.Get;
        
        var response = await resClient.ExecuteAsync(request);

        var deserializedResponse = JsonConvert.DeserializeObject<RecordModel>(response.Content);
        return deserializedResponse;
    }
   
    public async Task<long> CreateNewRecord(RecordModel recordModel, string token)
    {
        var resClient = new RestClient(UrlHelper.DmApiUrl + Path);

        var requestPath = UrlHelper.DmApiUrl + Path;
        var request = new RestRequest(requestPath);
        request.AddHeader("Authentication", $"Bearer {token!}");
        request.AddJsonBody(recordModel);
        request.Method = Method.Post;
        
        RequestHelper.WriteCreateRequestToSQLite(request, requestPath); // запись запроса в SQLite в Offline
        
        var response = await resClient.ExecuteAsync(request);
        var deserializedResponse = JsonConvert.DeserializeObject<long>(response.Content);
        return deserializedResponse;
    }
    
    public async Task<bool> UpdateRecord(RecordModel recordModel, string token) // TODO: в RequestHelper добавить метод для записи PUT запросов в SQLite
    {
        var resClient = new RestClient(UrlHelper.DmApiUrl + Path)
        {
            Authenticator = new JwtAuthenticator(token!)
        };
        
        var request = new RestRequest();
        request.AddJsonBody(recordModel);
        var response = await resClient.ExecutePutAsync(request);

        var deserializedResponse = JsonConvert.DeserializeObject<bool>(response.Content);
        return deserializedResponse;
    }
   
    public async Task<bool> DeleteRecord(long recordId, string token)
    {
        var resClient = new RestClient();

        var requestPath = UrlHelper.DmApiUrl + Path + $"?recordId={recordId}";
        var request = new RestRequest(requestPath);
        request.AddHeader("Authentication", $"Bearer {token!}");
        request.Method = Method.Delete;
      
        RequestHelper.WriteDeleteRequestToSQLite(request, requestPath, recordId); // запись запроса в SQLite

        var response = await resClient.ExecuteAsync(request);

        var deserializedResponse = JsonConvert.DeserializeObject<bool>(response.Content);
        return deserializedResponse;
    }
}
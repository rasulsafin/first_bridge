using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using WrapperDM.Helpers;
using WrapperDM.Models;

namespace WrapperDM.Controllers;

public class TemplateController
{
    private MemoryCache<Dictionary<long, TemplateModel>> _templateCache;
    private const string TemplateKey = "TemplateKey";
    private const string Path = "api/template";
    private const string RequestPath = UrlHelper.DmApiUrl + Path;

    public TemplateController()
    {
        _templateCache = new MemoryCache<Dictionary<long, TemplateModel>>();
    }

    public async Task<List<TemplateModel>> GetProjectTemplateOfRecord(long projectId, string token)
    {
        // получаем из кэша
        if (!OfflineHelper.CheckForInternetConnection())
        {
            var templates = _templateCache.GetSection(TemplateKey).Values.ToList();

            return templates;
        }
        
        var resClient = new RestClient();
        
        var request = new RestRequest(RequestPath + $"?projectId={projectId}");
        request.AddHeader("Authentication", $"Bearer {token!}");
        request.Method = Method.Get;

        try
        {
            var response = await resClient.ExecuteAsync(request);
            var deserializedResponse = JsonConvert.DeserializeObject<List<TemplateModel>>(response.Content);

            // добавляем в кэш
            var recordDictionary = new Dictionary<long, TemplateModel>();

            foreach (var template in deserializedResponse) // добавляем в словарь ключ/значение
            {
                var modelForCaching = new TemplateModel();
                modelForCaching.Id = template.Id;
                modelForCaching.Name = template.Name;
                modelForCaching.Project = template.Project;
                modelForCaching.ProjectId = template.ProjectId;
                modelForCaching.RecordTemplate = template.RecordTemplate;

                recordDictionary.Add(modelForCaching.Id, modelForCaching);
            }
         
            _templateCache.GetOrCreate(TemplateKey, () => recordDictionary);
            return deserializedResponse;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
   
    public async Task<bool> AddTemplateToProject(TemplateModel templateModel, string token)
    {
        var resClient = new RestClient(UrlHelper.DmApiUrl + Path);
        
        var request = new RestRequest();
        request.AddHeader("content-type", "application/json");
        request.AddHeader("Authentication", $"Bearer {token!}");
        request.AddJsonBody(templateModel);
        request.Method = Method.Post;
        
        RequestHelper.WriteCreateRequestToSQLite(request, RequestPath); // запись запроса в SQLite в Offline

        var response = await resClient.ExecuteAsync(request);
        var deserializedResponse = JsonConvert.DeserializeObject<bool>(response.Content);
        return deserializedResponse;
    }
    
    public async Task<bool> EditExistingTemplateOfProject(TemplateModelForEdit templateModelForEdit, string token)
    {
        var resClient = new RestClient();
        
        var request = new RestRequest(RequestPath);
        request.AddHeader("Authentication", $"Bearer {token!}");
        request.AddHeader("content-type", "application/json");
        request.AddJsonBody(templateModelForEdit);
        request.Method = Method.Put;
        var response = await resClient.ExecuteAsync(request);

        var deserializedResponse = JsonConvert.DeserializeObject<bool>(response.Content);
        return deserializedResponse;
    }
}
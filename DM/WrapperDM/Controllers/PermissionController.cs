using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using WrapperDM.Entities;
using WrapperDM.Helpers;
using WrapperDM.Models;

namespace WrapperDM.Controllers;

public class PermissionController
{
    private MemoryCache<Dictionary<long, PermissionEntity>> _permissionCache;
    private const string PermissionKey = "PermissionKey";
    private const string Path = "api/permission";

    public PermissionController()
    {
        _permissionCache = new MemoryCache<Dictionary<long, PermissionEntity>>();
    }

    public async Task<List<PermissionEntity>> GetAllPermissionsOfUser(long userId, string token)
    {
        // получаем из кэша
        if (!OfflineHelper.CheckForInternetConnection())
        {
            var permissions = _permissionCache.GetSection(PermissionKey).Values.ToList(); // лист объектов

            return permissions;
        }
        
        var resClient = new RestClient();

        var requestPath = UrlHelper.DmApiUrl + Path + $"?userId={userId}";
        var request = new RestRequest(requestPath);
        request.Method = Method.Get;
        request.AddHeader("Authentication", $"Bearer {token!}");

        try
        {
            var response = await resClient.ExecuteAsync(request);

            var deserializedResponse = JsonConvert.DeserializeObject<List<PermissionEntity>>(response.Content);
            
            // добавляем в кэш
            var permissionDictionary = new Dictionary<long, PermissionEntity>();

            foreach (var permissionModel in deserializedResponse) // добавляем в словарь ключ/значение
            {
                var modelForCaching = new PermissionEntity();
                modelForCaching.Id = permissionModel.Id;
                modelForCaching.Create = permissionModel.Create;
                modelForCaching.Delete = permissionModel.Delete;
                modelForCaching.Read = permissionModel.Read;
                modelForCaching.Type = permissionModel.Type;
                modelForCaching.User = permissionModel.User;
                modelForCaching.ObjectId = permissionModel.ObjectId;
                modelForCaching.UserId = permissionModel.UserId;

                permissionDictionary.Add(modelForCaching.Id, modelForCaching);
            }
         
            _permissionCache.GetOrCreate(PermissionKey, () => permissionDictionary);
            
            return deserializedResponse;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
    
    public async Task<object> AddPermissionToUser(PermissionModel permissionModel, string token)
    {
        var resClient = new RestClient();
        
        var requestPath = UrlHelper.DmApiUrl + Path;
        var request = new RestRequest(requestPath);
        request.Method = Method.Post;
        request.AddHeader("Authentication", $"Bearer {token!}");
        request.AddJsonBody(permissionModel);
        
        RequestHelper.WriteCreateRequestToSQLite(request, requestPath); // запись запроса в SQLite
        
        var response = await resClient.ExecuteAsync(request);
        
        return response;
    }

    public async Task<object> DeletePermissionOfUser(PermissionModel permissionModel, string token)
    {
        var resClient = new RestClient();

        var requestPath = UrlHelper.DmApiUrl + Path;
        var request = new RestRequest(requestPath);
        request.Method = Method.Delete;
        request.AddHeader("Authentication", $"Bearer {token!}");
        request.AddJsonBody(permissionModel);
        var response = await resClient.ExecuteAsync(request);
        
        return response;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using WrapperDM.Helpers;
using WrapperDM.Models;

namespace WrapperDM.Controllers;

public class ItemController
{
    private MemoryCache<Dictionary<long, ItemModel>> _itemCache;
    private const string Path = "api/item";
    private const string ItemKey = "ItemKey";
    public const string RequestPath = UrlHelper.DmApiUrl + Path;

    public ItemController()
    {
        _itemCache = new MemoryCache<Dictionary<long, ItemModel>>();
    }

    public async Task<List<ItemModel>> GetAllItems(string token)
    {
        // получаем из кэша
        if (!OfflineHelper.CheckForInternetConnection())
        {
            var items = _itemCache.GetSection(ItemKey).Values.ToList(); // лист объектов

            return items;
        }
        var resClient = new RestClient();
        
        var request = new RestRequest(RequestPath);
        request.Method = Method.Get;
        request.AddHeader("Authentication", $"Bearer {token!}");

        try
        {
            var response = await resClient.ExecuteAsync(request);
            var deserializedResponse = JsonConvert.DeserializeObject<List<ItemModel>>(response.Content);
            
            // добавляем в кэш
            var itemDictionary = new Dictionary<long, ItemModel>();

            foreach (var item in deserializedResponse) // добавляем в словарь ключ/значение
            {
                var modelForCaching = new ItemModel();
                modelForCaching.Id = item.Id;
                modelForCaching.Name = item.Name;
                modelForCaching.ProjectId = item.ProjectId;
                modelForCaching.RelativePath = item.RelativePath;

                itemDictionary.Add(modelForCaching.Id, modelForCaching);
            }
         
            _itemCache.GetOrCreate(ItemKey, () => itemDictionary);
            return deserializedResponse;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
    
    public async Task<List<ItemModel>> DownloadItem(string fileName, string osType, string token)
    {
        var resClient = new RestClient();
        
        var request = new RestRequest(RequestPath);
        request.AddHeader("Authentication", $"Bearer {token!}");
        request.AddHeader("fileName", fileName);
        request.AddHeader("osType", osType);
        request.Method = Method.Get;
        
        var response = await resClient.ExecuteAsync(request);

        var deserializedResponse = JsonConvert.DeserializeObject<List<ItemModel>>(response.Content);
        return deserializedResponse;
    }
    
    public async Task<long> UploadItem(long project, string fileName, string filePath, string token)
    {
        var resClient = new RestClient();
        
        var request = new RestRequest(RequestPath);
        request.AddHeader("Authentication", $"Bearer {token!}");
        request.AddFile(fileName, filePath);
        request.Method = Method.Post;
        
        var response = await resClient.ExecuteAsync(request);

        var deserializedResponse = JsonConvert.DeserializeObject<long>(response.Content);
        return deserializedResponse;
    }
}
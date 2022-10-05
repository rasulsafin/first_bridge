using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using WrapperDM.Helpers;
using WrapperDM.Models;

namespace WrapperDM;

public class WrapperItemController
{
    private const string Path = "api/item";
    
    public async Task<List<ItemModel>> GetAllItems(string token)
    {
        var resClient = new RestClient(UrlHelper.DmApiUrl + Path)
        {
            Authenticator = new JwtAuthenticator(token!)
        };
        var request = new RestRequest();
        var response = await resClient.ExecuteGetAsync(request);

        var deserializedResponse = JsonConvert.DeserializeObject<List<ItemModel>>(response.Content);
        return deserializedResponse;
    }
    
    public async Task<List<ItemModel>> DownloadItem(string fileName, string osType, string token)
    {
        var resClient = new RestClient(UrlHelper.DmApiUrl + Path)
        {
            Authenticator = new JwtAuthenticator(token!)
        };
        var request = new RestRequest();
        var response = await resClient.ExecuteGetAsync(request);

        var deserializedResponse = JsonConvert.DeserializeObject<List<ItemModel>>(response.Content);
        return deserializedResponse;
    }
    
    public async Task<long> UploadItem(long project, string fileName, string filePath, string token)
    {
        var resClient = new RestClient(UrlHelper.DmApiUrl + Path)
        {
            Authenticator = new JwtAuthenticator(token!)
        };
        var request = new RestRequest();
        request.AddFile(fileName, filePath);
        var response = await resClient.ExecutePostAsync(request);

        var deserializedResponse = JsonConvert.DeserializeObject<long>(response.Content);
        return deserializedResponse;
    }
}
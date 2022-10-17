using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using WrapperDM.Helpers;
using WrapperDM.Models;

namespace WrapperDM;

public class WrapperRecordController
{
    private const string Path = "api/record";

    public async Task<List<RecordModel>> GetAllRecords(string token)
    {
        var resClient = new RestClient(UrlHelper.DmApiUrl + Path)
        {
            Authenticator = new JwtAuthenticator(token!)
        };
        
        var request = new RestRequest();
        var response = await resClient.ExecuteGetAsync(request);

        var deserializedResponse = JsonConvert.DeserializeObject<List<RecordModel>>(response.Content);
        return deserializedResponse;
    }
   
    public async Task<RecordModel> GetRecordById(long recordId, string token)
    {
        var resClient = new RestClient(UrlHelper.DmApiUrl + Path + $"/{recordId}")
        {
            Authenticator = new JwtAuthenticator(token!)
        };
        
        var request = new RestRequest();
        var response = await resClient.ExecuteGetAsync(request);

        var deserializedResponse = JsonConvert.DeserializeObject<RecordModel>(response.Content);
        return deserializedResponse;
    }
   
    public async Task<long> CreateNewRecord(RecordModel recordModel, string token)
    {
        var resClient = new RestClient(UrlHelper.DmApiUrl + Path)
        {
            Authenticator = new JwtAuthenticator(token!)
        };
        
        var request = new RestRequest();
        request.AddJsonBody(recordModel);
        var response = await resClient.ExecutePostAsync(request);

        var deserializedResponse = JsonConvert.DeserializeObject<long>(response.Content);
        return deserializedResponse;
    }
    
    public async Task<bool> UpdateRecord(RecordModel recordModel, string token)
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
        var resClient = new RestClient(UrlHelper.DmApiUrl + Path + $"?recordId={recordId}")
        {
            Authenticator = new JwtAuthenticator(token!)
        };
        
        var request = new RestRequest();

        var response = await resClient.ExecutePutAsync(request);

        var deserializedResponse = JsonConvert.DeserializeObject<bool>(response.Content);
        return deserializedResponse;
    }
}
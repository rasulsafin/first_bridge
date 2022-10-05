using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using WrapperDM.Entities;
using WrapperDM.Helpers;
using WrapperDM.Models;

namespace WrapperDM;

public class WrapperPermissionController
{
    private const string Path = "api/permission";
    
    public async Task<List<PermissionEntity>> GetAllPermissionsOfUser(long userId, string token)
    {
        var resClient = new RestClient(UrlHelper.DmApiUrl + Path + $"?userId={userId}")
        {
            Authenticator = new JwtAuthenticator(token!)
        };
        
        var request = new RestRequest();
        var response = await resClient.GetAsync(request);

        var deserializedResponse = JsonConvert.DeserializeObject<List<PermissionEntity>>(response.Content);
        return deserializedResponse;
    }
    
    public async Task<object> AddPermissionToUser(PermissionModel permissionModel, string token)
    {
        var resClient = new RestClient(UrlHelper.DmApiUrl + Path)
        {
            Authenticator = new JwtAuthenticator(token!)
        };
        var request = new RestRequest();
        request.AddJsonBody(permissionModel);
        var response = await resClient.PostAsync(request);
        
        return response;
    }

    public async Task<object> DeletePermissionOfUser(PermissionModel permissionModel, string token)
    {
        var resClient = new RestClient(UrlHelper.DmApiUrl + Path)
        {
            Authenticator = new JwtAuthenticator(token!)
        };
        var request = new RestRequest();
        request.AddJsonBody(permissionModel);
        var response = await resClient.DeleteAsync(request);
        
        return response;
    }
}
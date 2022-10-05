using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using WrapperDM.Entities;
using WrapperDM.Helpers;
using WrapperDM.Models;

namespace WrapperDM;

public class WrapperProjectController
{
    private const string Path = "api/project";

    public async Task<List<ProjectModel>> GetAllProjects(string token)
    {
        var resClient = new RestClient(UrlHelper.DmApiUrl + Path)
        {
            Authenticator = new JwtAuthenticator(token!)
        };
        
        var request = new RestRequest();
        var response = await resClient.GetAsync(request);

        var deserializedResponse = JsonConvert.DeserializeObject<List<ProjectModel>>(response.Content);
        return deserializedResponse;
    }
    
    public async Task<RestResponse> GetProjectById(long projectId, string token)
    {
        var resClient = new RestClient(UrlHelper.DmApiUrl + Path + $"/{projectId}")
        {
            Authenticator = new JwtAuthenticator(token!)
        };
        
        var request = new RestRequest();
        var response = await resClient.GetAsync(request);

      //  var deserializedResponse = JsonConvert.DeserializeObject<ProjectModel>(response.Content);
        return response;
    }

    public async Task<long> Create(ProjectModel projectModel, string token)
    {
        var resClient = new RestClient(UrlHelper.DmApiUrl + Path)
        {
            Authenticator = new JwtAuthenticator(token!)
        };
        
        var request = new RestRequest();
        request.AddJsonBody(projectModel);
        var response = await resClient.PostAsync(request);
    
        var deserializedResponse = JsonConvert.DeserializeObject<long>(response.Content);
        return deserializedResponse;
    }
}
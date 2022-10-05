using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using WrapperDM.Helpers;
using WrapperDM.Models;

namespace WrapperDM;

public class WrapperTemplateController
{
    private const string Path = "api/template";

    public async Task<List<TemplateModel>> GetProjectTemplateOfRecord(long projectId, string token)
    {
        var resClient = new RestClient(UrlHelper.DmApiUrl + Path + $"?projectId={projectId}")
        {
            Authenticator = new JwtAuthenticator(token!)
        };
        
        var request = new RestRequest();
        var response = await resClient.GetAsync(request);

        var deserializedResponse = JsonConvert.DeserializeObject<List<TemplateModel>>(response.Content);
        return deserializedResponse;
    }
   
    public async Task<bool> AddTemplateToProject(TemplateModel templateModel, string token)
    {
        var resClient = new RestClient(UrlHelper.DmApiUrl + Path)
        {
            Authenticator = new JwtAuthenticator(token!)
        };
        
        var request = new RestRequest();
        request.AddHeader("content-type", "application/json");
        request.AddJsonBody(templateModel);
        var response = await resClient.PostAsync(request);

        var deserializedResponse = JsonConvert.DeserializeObject<bool>(response.Content);
        return deserializedResponse;
    }
    
    public async Task<bool> EditExistingTemplateOfProject(TemplateModelForEdit templateModelForEdit, string token)
    {
        var resClient = new RestClient(UrlHelper.DmApiUrl + Path)
        {
            Authenticator = new JwtAuthenticator(token!)
        };
        
        var request = new RestRequest();
        request.AddHeader("content-type", "application/json");
        request.AddJsonBody(templateModelForEdit);
        var response = await resClient.PutAsync(request);

        var deserializedResponse = JsonConvert.DeserializeObject<bool>(response.Content);
        return deserializedResponse;
    }
}
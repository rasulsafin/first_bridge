using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using WrapperDM.Entities;
using WrapperDM.Helpers;
using WrapperDM.Models;

namespace WrapperDM;

public class WrapperOrganizationController
{
    private const string Path = "api/organization";

    public async Task<List<OrganizationEntity>> GetAllOrganizations(string token)
    {
        var resClient = new RestClient(UrlHelper.DmApiUrl + Path)
        {
            Authenticator = new JwtAuthenticator(token!)
        };
        var request = new RestRequest();
        var response = await resClient.GetAsync(request);

        var deserializedResponse = JsonConvert.DeserializeObject<List<OrganizationEntity>>(response.Content);
        return deserializedResponse;
    }
    
    public async Task<RestResponse> CreateOrganization(OrganizationModelForCreate organizationModel, string token)
    {
        var resClient = new RestClient(UrlHelper.DmApiUrl + Path)
        {
            Authenticator = new JwtAuthenticator(token!)
        };
        
        var request = new RestRequest();
        request.AddJsonBody(organizationModel);
        
        var response = await resClient.PostAsync(request);
        return response;
    }
}
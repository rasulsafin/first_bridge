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

public class OrganizationController
{
    private MemoryCache<Dictionary<long, OrganizationEntity>> _organizationCache;
    private const string OrganizationKey = "OrganizationKey";
    private const string Path = "api/organization";

    public OrganizationController()
    {
        _organizationCache = new MemoryCache<Dictionary<long, OrganizationEntity>>();
    }

    public async Task<List<OrganizationEntity>> GetAllOrganizations(string token)
    {
        // получаем из кэша
        if (!OfflineHelper.CheckForInternetConnection())
        {
            var organizations = _organizationCache.GetSection(OrganizationKey).Values.ToList(); // лист объектов

            return organizations;
        }
        
        var resClient = new RestClient();

        var requestPath = UrlHelper.DmApiUrl + Path;
        var request = new RestRequest(requestPath);
        request.Method = Method.Get;
        request.AddHeader("Authentication", $"Bearer {token!}");

        try
        {
            var response = await resClient.ExecuteAsync(request);
            var deserializedResponse = JsonConvert.DeserializeObject<List<OrganizationEntity>>(response.Content);
            
            // добавляем в кэш
            var recordDictionary = new Dictionary<long, OrganizationEntity>();

            foreach (var organization in deserializedResponse) // добавляем в словарь ключ/значение
            {
                var modelForCaching = new OrganizationEntity();
                modelForCaching.Id = organization.Id;
                modelForCaching.Name = organization.Name;
                modelForCaching.Address = organization.Address;
                modelForCaching.Email = organization.Email;
                modelForCaching.Inn = organization.Inn;
                modelForCaching.Kpp = organization.Kpp;
                modelForCaching.Ogrn = organization.Ogrn;
                modelForCaching.Phone = organization.Phone;
                modelForCaching.Projects = organization.Projects;
                modelForCaching.Users = organization.Users;

                recordDictionary.Add(modelForCaching.Id, modelForCaching);
            }
         
            _organizationCache.GetOrCreate(OrganizationKey, () => recordDictionary);
            return deserializedResponse;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
    
    public async Task<long> CreateOrganization(OrganizationModelForCreate organizationModel, string token)
    {
        var resClient = new RestClient();

        var requestPath = UrlHelper.DmApiUrl + Path;
        var request = new RestRequest(requestPath);
        request.AddHeader("Authentication", $"Bearer {token!}");
        request.AddJsonBody(organizationModel);
        request.Method = Method.Post;
        
        RequestHelper.WriteCreateRequestToSQLite(request, requestPath); // запись запроса в SQLite в Offline
        var response = await resClient.ExecuteAsync(request);
        var deserializedResponse = JsonConvert.DeserializeObject<long>(response.Content);
        return deserializedResponse;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using WrapperDM.Helpers;
using WrapperDM.Models;

namespace WrapperDM.Controllers;

public class ProjectController
{
    private MemoryCache<Dictionary<long, ProjectModel>> _projectCache;
    private const string Path = "api/project";
    private const string ProjectKey = "projectCache";

    public ProjectController()
    {
        _projectCache = new MemoryCache<Dictionary<long, ProjectModel>>();
    }

    public async Task<List<ProjectModel>> GetAllProjects(string token)
    {
        // получаем из кэша
        if (!OfflineHelper.CheckForInternetConnection())
        {
            var projects = _projectCache.GetSection(ProjectKey).Values.ToList(); // лист объектов

            return projects;
        }
        
        var resClient = new RestClient();

        var requestPath = "UrlHelper.DmApiUrl + Path";
        var request = new RestRequest();
        request.AddHeader("Authentication", $"Bearer {token!}");
        request.Method = Method.Get;

        try
        {
            var response = await resClient.ExecuteAsync(request);
            var deserializedResponse = JsonConvert.DeserializeObject<List<ProjectModel>>(response.Content);
            
            // добавляем в кэш
            var projectDictionary = new Dictionary<long, ProjectModel>();

            foreach (var projectModel in deserializedResponse) // добавляем в словарь ключ/значение
            {
                var modelForCaching = new ProjectModel();
                modelForCaching.Id = projectModel.Id;
                modelForCaching.Title = projectModel.Title;
                modelForCaching.OrganizationId = projectModel.OrganizationId;
                modelForCaching.Description = projectModel.Description;

                projectDictionary.Add(modelForCaching.Id, modelForCaching);
            }
         
            _projectCache.GetOrCreate(ProjectKey, () => projectDictionary);
            return deserializedResponse;
        }
        catch (SocketException e)
        {
            Console.WriteLine("отловлен SocketException");
            return null;
        }
    }
    
    public async Task<ProjectModel> GetProjectById(long projectId, string token)
    {
        // получаем из кэша, если было получено в GetAll
        if (!OfflineHelper.CheckForInternetConnection()) // TODO: добавить проверку токена
        {
            var project = _projectCache.GetSection(ProjectKey)
                .Where(x => x.Value.Id == projectId)
                .Select(x => x.Value).FirstOrDefault();
            return project;
        }
        
        var resClient = new RestClient();
        var requestPath = UrlHelper.DmApiUrl + Path + $"/{projectId}";
        var request = new RestRequest(requestPath);
        request.AddHeader("projectId", projectId);
        request.AddHeader("Authentication", $"Bearer {token!}");
        request.Method = Method.Get;
        
        var response = await resClient.ExecuteAsync(request);
        var deserializedResponse = JsonConvert.DeserializeObject<ProjectModel>(response.Content);
        
        return deserializedResponse;
    }

    public async Task<long> Create(ProjectModel projectModel, string token)
    {
        var resClient = new RestClient();

        var requestPath = UrlHelper.DmApiUrl + Path;
        var request = new RestRequest(requestPath);
        request.AddHeader("Authentication", $"Bearer {token!}");
        request.Method = Method.Post;
        request.AddJsonBody(projectModel);
        
        RequestHelper.WriteCreateRequestToSQLite(request, requestPath); // запись запроса в SQLite
        var response = await resClient.ExecuteAsync(request);
    
        var deserializedResponse = JsonConvert.DeserializeObject<long>(response.Content);
        return deserializedResponse;
    }
}
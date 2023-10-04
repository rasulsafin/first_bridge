using Microsoft.Extensions.Configuration;
using offline_module.Domain.Interfaces;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dotmim.Sync.Web.Client;
using Dotmim.Sync.PostgreSql;
using Dotmim.Sync;
using Dotmim.Sync.Enumerations;
using Xbim.IO.Xml.BsConf;
using DM.Domain.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DM.Domain.DTO;
using System.Text.Json;
using Newtonsoft.Json;

namespace offline_module.Domain.Services
{
    public class SyncDotMimService : ISyncDotMimService
    {
        public IConfiguration configuration;
        public string _serverProvider;
        public int _syncIntervalSeconds;
        public string _clientConnectionString;
        public string _bearerToken;
        public IBearerConfigService _bearerConfigService;

        public SyncDotMimService(IConfiguration configuration, IBearerConfigService bearerConfigService)
        {
            var minioConfigSection = configuration.GetSection("DotMimConfig");
            _serverProvider = minioConfigSection["ServerProvider"];
            _syncIntervalSeconds = int.Parse(minioConfigSection["SyncIntervalInSeconds"]);
            _clientConnectionString = minioConfigSection["ClientConnectionString"];
            _bearerConfigService = bearerConfigService;
        }

        public async Task SynchronizeAsync()
        {
            _bearerToken = await GenerateJwtToken();

            var httpClient = new HttpClient();


            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);

            var serverProvider = new WebRemoteOrchestrator($"{_serverProvider}/api/sync", client: httpClient);
            var clientProvider = new NpgsqlSyncProvider(_clientConnectionString);

            var agent = new SyncAgent(clientProvider, serverProvider);

            var localOrchestrator = agent.LocalOrchestrator;
            var remoteOrchestrator = agent.RemoteOrchestrator;

            var tables = new SyncSetup(new string[] {"Comment", "Field", "Item", "List", "ListField",
                "Organization", "Permission", "Project", "Record", "Role", "Template", "UserProject", "User"});

            CancellationTokenSource cts = null;

            var progress = new SynchronousProgress<ProgressArgs>(s =>
                Console.WriteLine($"{s.ProgressPercentage:p}:  " +
                $"\t[{s?.Source?[..Math.Min(4, s.Source.Length)]}] {s.TypeName}: {s.Message}"));

            try
            {
                Console.Clear();

                // Launch the sync process
                cts = new CancellationTokenSource();

                var results = await agent.SynchronizeAsync(scopeName: "scope", tables, SyncType.Normal, null, cts.Token, progress);

                Console.WriteLine($"Sync duration: {results.CompleteTime.Subtract(results.StartTime).TotalSeconds}\n" +
                    $"Changes: {results.TotalChangesUploadedToServer}\n" +
                    $"Downloaded: {results.TotalChangesDownloadedFromServer}\n" +
                    $"Applied: {results.TotalChangesAppliedOnServer}\n" +
                    $"Conflicts: {results.TotalResolvedConflicts}");

                Console.WriteLine();
                results = await agent.SynchronizeAsync();

                Console.WriteLine($"Sync duration: {results.CompleteTime.Subtract(results.StartTime).TotalSeconds}\n" +
                    $"Changes: {results.TotalChangesUploadedToServer}\n" +
                    $"Downloaded: {results.TotalChangesDownloadedFromServer}\n" +
                    $"Applied: {results.TotalChangesAppliedOnServer}\n" +
                    $"Conflicts: {results.TotalResolvedConflicts}");

                Thread.Sleep(TimeSpan.FromSeconds(_syncIntervalSeconds)); // Wait for n seconds before the next 
            }
            catch
            {
                throw;
            }
        }
        private async Task<string> GenerateJwtToken()
        {
            var httpClient = new HttpClient();

            var authReq = new AuthenticateRequest()
            {
                Login = "string",
                Password = "string",
            };

            var jsonData = JsonConvert.SerializeObject(authReq);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{_serverProvider}/api/users/authenticate", content);

            var newContent = await response.Content.ReadAsStringAsync();

            var AuthResp = JsonConvert.DeserializeObject<AuthenticateResponse>(newContent);

            return AuthResp.Token;
        }
    }
}


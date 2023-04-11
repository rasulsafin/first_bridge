using Dotmim.Sync.Enumerations;
using Dotmim.Sync.PostgreSql;
using Dotmim.Sync.Web.Client;
using Dotmim.Sync;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Threading;

namespace DMClientSync
{
    public class Program
    {
        private static string clientConnectionString =
            "Server=localhost;Port=5432;UserId=postgres;Password=123;Database=dm3;";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Be sure the web api has started. Then click enter..");
            Console.ReadLine();
            await SynchronizeAsync();
        }

        private static async Task SynchronizeAsync()
        {
            var token = GenerateJwtToken("string", "string");

            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var serverProvider = new WebRemoteOrchestrator("https://localhost:5001/api/sync", client: httpClient);
            var clientProvider = new NpgsqlSyncProvider(clientConnectionString);

            var agent = new SyncAgent(clientProvider, serverProvider);

            var localOrchestrator = agent.LocalOrchestrator;
            var remoteOrchestrator = agent.RemoteOrchestrator;

            var tables = new SyncSetup(new string[] { "Comments", "Field", "Items", "List", "ListField",
                                                      "Organization", "Permissions", "Projects", "RecordField", "RecordList",
                                                      "Records", "Role", "Template", "TemplateField", "TemplateList", "UserProjectEntity", "Users"});
            CancellationTokenSource cts = null;

            var progress = new SynchronousProgress<ProgressArgs>(s =>
                Console.WriteLine($"{s.ProgressPercentage:p}:  " +
                $"\t[{s?.Source?[..Math.Min(4, s.Source.Length)]}] {s.TypeName}: {s.Message}"));

            do
            {
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

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (Console.ReadKey().Key != ConsoleKey.Escape);

            Console.WriteLine("End");
        }

        private static string GenerateJwtToken(string email, string userid)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Iss, "Dotmim.Sync.Bearer"),
                new Claim(JwtRegisteredClaimNames.Aud, "Dotmim.Sync.Bearer"),
                new Claim(JwtRegisteredClaimNames.Sub, "Dotmim.Sync"),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userid)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SOME_RANDOM_KEY_DO_NOT_SHARE"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddDays(Convert.ToDouble(10));

            var token = new JwtSecurityToken(
                "Dotmim.Sync.Bearer",
                "Dotmim.Sync.Bearer",
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

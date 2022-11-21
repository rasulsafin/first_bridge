using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using RestSharp;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WrapperDM.Helpers;

public static class RequestHelper
{
    public static RestRequest GetRequest(string url, Method method)
    {
        return new RestRequest(url, method);
    }

    public static void WriteDeleteRequestToSQLite(RestRequest request, string requestPath, long id)
    {
        if (!OfflineHelper.CheckForInternetConnection())
        {
            var headers = request.Parameters
                .Where(x => x.Type == ParameterType.HttpHeader)
                .Select(x => new KeyValuePair<string,string>(x.Name, x.Value.ToString()))
                .ToList(); // в RestSharpе хэдэры приватные и нельзя вытащить напрямую
         
            using (var connection = new SqliteConnection($"Data Source={SqliteDatabaseContext.DatabaseName}"))
            {
                connection.Open();

                var headersForSQLite = headers[0].ToString().Replace("[", string.Empty).Replace("]", string.Empty);
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText =
                    $"INSERT INTO SavedRequests (Body, Headers, Path, Method) VALUES " +
                    $"({id}, \"{headersForSQLite}\", \"{requestPath}\", \"{request.Method.ToString()}\")";
                command.ExecuteNonQuery();
            }
        }
    }
    
    public static void WriteCreateRequestToSQLite(RestRequest request, string requestPath)
    {
        if (request == null || string.IsNullOrWhiteSpace(requestPath))
        {
            return;
        }

        if (!OfflineHelper.CheckForInternetConnection())
        {
            var body = request.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            if (body == null)
            {
                return;
            }

            var bodyWithModel = JsonSerializer.Serialize(body.Value).Replace("\"", "\"\"");
            
            using (var connection = new SqliteConnection($"Data Source={SqliteDatabaseContext.DatabaseName}"))
            {
                connection.Open();
                
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText =
                    $"INSERT INTO SavedRequests (Body,  Path, Method) VALUES " +
                    $"(\"{bodyWithModel}\", \"{requestPath}\", \"{request.Method.ToString()}\")";
                command.ExecuteNonQuery();
            }
        }
    }
}
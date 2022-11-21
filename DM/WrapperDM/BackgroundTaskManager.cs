using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using RestSharp;
using WrapperDM.Entities;
using WrapperDM.Helpers;

namespace WrapperDM;

public class BackgroundTaskManager // POST, PUT, DELETE
{
    public const string SavedRequestsTable = "SavedRequests";
    public void StartWorker()
    {
        var delay = TimeSpan.FromSeconds(10);

        // TODO: поменять условие после проверки
        Task.Run(async () =>
            {
                try
                {
                    if (OfflineHelper.CheckForInternetConnection()) await Execute(); // OfflineHelper.CheckForInternetConnection()
                    Console.WriteLine("try");

                    await Task.Delay(delay);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        );
    }

    public async Task Execute()
    {
        var limit = 10;
        using (var connection = new SqliteConnection($"Data Source={SqliteDatabaseContext.DatabaseName}"))
        {
            connection.Open();
            var command = new SqliteCommand();
            command.Connection = connection;

            command.CommandText = @$"SELECT * FROM {SavedRequestsTable} ORDER BY CreatedAt LIMIT {limit}";

            var reader = command.ExecuteReader();
            var savedRequests = new List<SavedRequestEntity>();

            using (reader)
            {
                if (!reader.HasRows) return; // если нет данных

                while (reader.Read()) // построчно считываем данные
                {
                    var savedRequest = new SavedRequestEntity();
                    foreach (var f in reader)
                    {
                        savedRequest.Body = (string)reader[0]; // body
                        //заполнить все поля
                    }

                    Console.WriteLine(savedRequest);
                    savedRequests.Add(savedRequest);
                }
            }

            foreach (var item in savedRequests)
            {
                using var client = new RestClient();
                var request = new RestRequest(item.Path);
                request.Method = item.Method;
                
                if (item.Headers == null && !string.IsNullOrEmpty(item.Body)) // для CREATE запросов
                {
                    request.AddJsonBody(item.Body); // AddStringBody если не работает
                }
                
                var headers = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(item.Headers);

                if (headers != null) // DELETE
                {
                    request.AddHeaders(headers);

                    if (!string.IsNullOrEmpty(item.Body))
                    {
                        request.AddStringBody(item.Body, DataFormat.Json);
                    }
                }

                try
                {
                    await client.ExecuteAsync(request);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                
            }
            connection.Close(); // нужно ли?
        }

        // TODO: Проверить работоспособность
        // удаление всех записей после того, как запросы отработали
        using (var connection = new SqliteConnection($"Data Source={SqliteDatabaseContext.DatabaseName}"))
        {
            connection.Open();
            var command = new SqliteCommand();
            command.Connection = connection;

            command.CommandText = @$"DELETE * FROM {SavedRequestsTable}";

            command.ExecuteNonQuery();

        }
    }
}
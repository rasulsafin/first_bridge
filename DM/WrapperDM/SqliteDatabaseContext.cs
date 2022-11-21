using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;

namespace WrapperDM;

public static class SqliteDatabaseContext
{
    public const string DatabaseName = "uniqueDb.db";
    
    public static void Seed()
    {
        // пересоздавать нет необходимости
        if (File.Exists(DatabaseName))
        {
            return;
        }
        
        // TODO: поменять на ORM при необходимости
        using (var connection = new SqliteConnection($"Data Source={DatabaseName}"))
        {
            connection.Open();
            
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = "CREATE TABLE SavedRequests(_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, " +
                                  "Body TEXT NOT NULL, Headers TEXT, Path TEXT NOT NULL, Method TEXT NOT NULL)";
            command.ExecuteNonQuery();

            command.CommandText =
                "CREATE TABLE Users(_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL, LastName TEXT NOT NULL," +
                "FathersName TEXT NOT NULL, Login TEXT, Email TEXT, Roles TEXT, Birthdate TEXT, Snils NOT NULL)";
            command.ExecuteNonQuery();

            command.CommandText = "CREATE TABLE Records(_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, " +
                                  "Name TEXT NOT NULL, ProjectId INTEGER NOT NULL, FIELDS JSON NOT NULL)";
            command.ExecuteNonQuery();

            command.CommandText = "CREATE TABLE Projects(_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Title TEXT, Description TEXT)";
            command.ExecuteNonQuery();
            // command.Dispose();

            Console.WriteLine($"Таблица {DatabaseName} создана");
        }
    }

    // возможна ошибка: database is locked — база уже открыта и используется (например через DbBrowser(SQLite))
    // public int AddInfoAboutRequestToTable(params object[] request) // first METHOD, second TABLE, third BODY of request
    // {
    //     if (request == null)
    //     {
    //         return -1;
    //     }
    //     
    //     Console.WriteLine(request.Length);
    //     
    //     using (var connection = new SqliteConnection($"Data Source={DatabaseName}"))
    //     {
    //         connection.Open();
    //         
    //         SqliteCommand command = new SqliteCommand();
    //         command.Connection = connection;
    //
    //         string requestJson = "\"";
    //
    //         // добавляем все значения в строку для SQL Query
    //
    //         if (request.Length == 1) // проверка для оптимизации
    //         {
    //             requestJson += request[request.Length];
    //         }
    //         
    //         for (var i = 0; i < request.Length - 1; i++)
    //         {
    //             requestJson += request[i] + ", ";
    //         }
    //         
    //         requestJson += request[request.Length - 1] + "\"";
    //
    //         command.CommandText = @$"INSERT INTO RequestData(Value) VALUES ({requestJson})";
    //         Console.WriteLine(command.CommandText);
    //         var number = command.ExecuteNonQuery(); // сколько объектов добавлено в таблицу
    //         
    //         return number;
    //     }
    // }
    //
    // public string GetInfoAboutRequest(string condition = "")
    // {
    //     if (condition == null)
    //     {
    //         return null;
    //     }
    //
    //     using (var connection = new SqliteConnection($"Data Source={DatabaseName}"))
    //     {
    //         connection.Open();
    //         
    //         var command = new SqliteCommand();
    //         command.Connection = connection;
    //         
    //         command.CommandText = @$"SELECT * FROM RequestData WHERE ({condition})";
    //         Console.WriteLine(command.CommandText);
    //
    //         var result = command.ExecuteReader();
    //         
    //         using (SqliteDataReader reader = command.ExecuteReader())
    //         {
    //             if (reader.HasRows) // если есть данные
    //             {
    //                 while (reader.Read())   // построчно считываем данные
    //                 {
    //                     var id = reader.GetValue(0);
    //                     var value = reader.GetValue(1);
    //
    //                     Console.WriteLine($"{id} \t {value} \t");
    //                 }
    //             }
    //
    //             return reader.ToString();
    //         }
    //         
    //     }
    // }
    //
    // public string GetAllFromTable(string table)
    // {
    //     if (table == null || table.Length == 0)
    //     {
    //         return null;
    //     }
    //
    //     using (var connection = new SqliteConnection($"Data Source={DatabaseName}"))
    //     {
    //         connection.Open();
    //         
    //         var command = new SqliteCommand();
    //         command.Connection = connection;
    //         
    //         command.CommandText = @$"SELECT * FROM {table}";
    //         Console.WriteLine(command.CommandText);
    //
    //         var reader = command.ExecuteReader();
    //         
    //         using (reader)
    //         {
    //             var result = new List<object>();
    //             if (!reader.HasRows) return null; // если есть данные
    //             while (reader.Read())   // построчно считываем данные
    //             {
    //                 foreach (var f in reader)
    //                 {
    //                     result.Add(f);
    //                     Console.WriteLine(f + "was added");
    //                 }
    //             }
    //
    //             Console.WriteLine(result);
    //             return result.ToString();
    //         }
    //         
    //     }
    // }

}
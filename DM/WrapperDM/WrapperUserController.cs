using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using WrapperDM.Helpers;
using WrapperDM.Models;

namespace WrapperDM;

public class WrapperUserController
{
   MemoryCache<Dictionary<long, UserModel>> _usersCache = new();
   private const string UsersKey = "userCache";
   private const string Path = "api/users";
   
   public async Task<AuthenticateResponse> Authenticate(string login, string password)
   {
      if (!OfflineHelper.CheckForInternetConnection())
      {
         // TODO: добавить кэширование токена
      }
      
      var resClient = new RestClient();
      var requestPath = UrlHelper.DmApiUrl + Path + "/authenticate";
      var request = new RestRequest(requestPath);
      request.AddHeader("login", $"{login}");
      request.AddHeader("password", $"{password}");
      request.Method = Method.Post;

      try
      {
         var response = await resClient.ExecuteAsync(request);
         var deserializedResponse = JsonConvert.DeserializeObject<AuthenticateResponse>(response.Content);
         return deserializedResponse;
      }
      catch (SocketException e)
      {
         Console.WriteLine(e);
         return null;
      }
   }

   public async Task<List<UserModel>> GetAllUsers(string token)
   {
      var resClient = new RestClient();
      var requestPath = UrlHelper.DmApiUrl + Path;
      var request = new RestRequest(requestPath);
      request.AddHeader("Authentication", $"Bearer {token!}");
      request.Method = Method.Get;

      // получаем из кэша
      if (!OfflineHelper.CheckForInternetConnection())
      {
         var users = _usersCache.GetSection(UsersKey).Values.ToList();

         return users;
      }
      
      try
      {
         var response = await resClient.ExecuteAsync(request);
         var deserializedResponse = JsonConvert.DeserializeObject<List<UserModel>>(response.Content);

         // добавляем в кэш
         var userDictionary = new Dictionary<long, UserModel>();

         foreach (var userModel in deserializedResponse) // добавляем в словарь ключ/значение
         {
            var modelForCaching = new UserModel();
            modelForCaching.Id = userModel.Id;
            modelForCaching.Name = userModel.Name;
            modelForCaching.Login = userModel.Login;
            modelForCaching.Position = userModel.Position;
            modelForCaching.Email = userModel.Email;
            modelForCaching.Birthdate = userModel.Birthdate;
            
            userDictionary.Add(modelForCaching.Id, modelForCaching);
         }
         
         _usersCache.GetOrCreate("userCache", () => userDictionary);
         
         return deserializedResponse;
      }
      catch (SocketException)
      {
         Console.WriteLine("отловлен SocketException");
            
         return null;
      }
   }

   public async Task<List<UserModel>> GetFromCacheChecker() // для проверки
   {
      var users = _usersCache.GetSection(UsersKey).Values.ToList();

      return users;
   }

   public async Task<UserModel> GetUserById(long userId, string token)
   {
      var resClient = new RestClient(UrlHelper.DmApiUrl + Path + $"/{userId}")
      {
         Authenticator = new JwtAuthenticator(token!)
      };
      
      var request = new RestRequest();
      request.AddHeader("userId", userId);
      
      var response = await resClient.GetAsync(request);
      var deserializedResponse = JsonConvert.DeserializeObject<UserModel>(response.Content);
      return deserializedResponse;
   }
   
   public async Task<bool> CreateNewUser(UserModel userModel, string token)
   {
      var resClient = new RestClient(UrlHelper.DmApiUrl + Path)
      {
         Authenticator = new JwtAuthenticator(token!)
      };
      var request = new RestRequest();
      request.AddJsonBody(userModel);
      
      var response = await resClient.ExecutePostAsync(request);
      var deserializedResponse = JsonConvert.DeserializeObject<bool>(response.Content);
      return deserializedResponse;
   }
   
   public async Task<object> DeleteUser(int userId, string token)
   {
      var resClient = new RestClient();
      var requestPath = UrlHelper.DmApiUrl + Path + $"?userId={userId}";
      var request = new RestRequest(requestPath);
      request.AddHeader("Authentication", $"Bearer {token!}");
      request.Method = Method.Delete;

      // TODO: поменять условие
      if (!OfflineHelper.CheckForInternetConnection()) // !OfflineHelper.CheckForInternetConnection()
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
               $"({userId}, \"{headersForSQLite}\", \"{requestPath}\", \"{request.Method.ToString()}\")";
            command.ExecuteNonQuery();
         }

         return null;
      }

      try
      {
         var response = await resClient.ExecuteAsync(request);
         return null;
      }
      catch (SocketException e)
      {
         Console.WriteLine(e);
         return null;
      }
   }
}
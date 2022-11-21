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

public class UserController
{
   private MemoryCache<Dictionary<long, UserModel>> _usersCache;
   private MemoryCache<AuthenticateResponse> _tokenCache; // TODO: засунуть в кэш юзеров?
 
   private const string UsersKey = "userCache";
   private const string TokenKey = "tokenCache";
   private const string Path = "api/users";

   public UserController()
   {
      _usersCache = new MemoryCache<Dictionary<long, UserModel>>();
      _tokenCache = new MemoryCache<AuthenticateResponse>();
   }

   public async Task<AuthenticateResponse> Authenticate(string login, string password)
   {
      if (!OfflineHelper.CheckForInternetConnection())
      {
         var token = _tokenCache.GetSection(TokenKey);
         return token;
      }
      
      var resClient = new RestClient();
      var requestPath = UrlHelper.DmApiUrl + Path + "/authenticate";
      var request = new RestRequest(requestPath);
      request.AddHeader("content-type", "application/json");
      request.AddJsonBody(new
      {
         Login = login,
         Password = password
      });

      request.Method = Method.Post;

      try
      {
         var response = await resClient.ExecuteAsync(request);
         var deserializedResponse = JsonConvert.DeserializeObject<AuthenticateResponse>(response.Content);

         _tokenCache.GetOrCreate(TokenKey, () => (deserializedResponse));
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
      // получаем из кэша
      if (!OfflineHelper.CheckForInternetConnection())
      {
         var users = _usersCache.GetSection(UsersKey).Values.ToList(); // лист объектов

         return users;
      }
      
      var resClient = new RestClient();
      var requestPath = UrlHelper.DmApiUrl + Path;
      var request = new RestRequest(requestPath);
      request.AddHeader("Authentication", $"Bearer {token!}");
      request.Method = Method.Get;

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
            modelForCaching.LastName = userModel.LastName;
            modelForCaching.FathersName = userModel.FathersName;
            modelForCaching.OrganizationId = userModel.OrganizationId;
            modelForCaching.Snils = userModel.Snils;
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

   public async Task<UserModel> GetUserById(long userId, string token) 
   {
      // получаем из кэша, если было получено в GetAll
      if (!OfflineHelper.CheckForInternetConnection()) // TODO: добавить проверку токена
      {
         var user = _usersCache.GetSection(UsersKey)
            .Where(x => x.Value.Id == userId)
            .Select(x => x.Value).FirstOrDefault();

         return user;
      }

      var resClient = new RestClient();
      var requestPath = UrlHelper.DmApiUrl + Path + $"/{userId}";
      var request = new RestRequest(requestPath);
      request.AddHeader("userId", userId);
      request.AddHeader("Authentication", $"Bearer {token!}");
      request.Method = Method.Get;
      
      var response = await resClient.ExecuteAsync(request);
      var deserializedResponse = JsonConvert.DeserializeObject<UserModel>(response.Content);
      return deserializedResponse;
   }
   
   public async Task<bool> CreateNewUser(UserModel userModel, string token)
   {
      var resClient = new RestClient();
      var requestPath = UrlHelper.DmApiUrl + Path;
      var request = new RestRequest(requestPath);
      request.AddJsonBody(userModel);
      request.Method = Method.Post;

      RequestHelper.WriteCreateRequestToSQLite(request, requestPath); // запись запроса в SQLite в Offline

      var response = await resClient.ExecuteAsync(request);
      var deserializedResponse = JsonConvert.DeserializeObject<bool>(response.Content);
      return deserializedResponse;
   }
   
   public async void DeleteUser(int userId, string token)
   {
      var resClient = new RestClient();
      var requestPath = UrlHelper.DmApiUrl + Path + $"?userId={userId}";
      var request = new RestRequest(requestPath);
      request.AddHeader("Authentication", $"Bearer {token!}");
      request.Method = Method.Delete;
      
      RequestHelper.WriteDeleteRequestToSQLite(request, requestPath, userId); // запись запроса в SQLite

      try
      {
         await resClient.ExecuteAsync(request);
      }
      catch (SocketException e)
      {
         Console.WriteLine(e);
      }
   }
   //..
   
   // TODO: удалить перед деплоем
   public async Task<List<UserModel>> GetFromUserCacheChecker() // для проверки юзеров
   {
      var users = _usersCache.GetSection(UsersKey).Values.ToList();

      return users;
   }
   
   public async Task<AuthenticateResponse> GetFromTokenCacheChecker() // для проверки токена
   {
      var response = _tokenCache.GetSection(TokenKey);

      return response;
   }

   public UserModel GetUserByIdFromCacheChecker(long userId)
   {
      var user = _usersCache.GetSection(UsersKey)
         .Where(x => x.Value.Id == userId)
         .Select(x => x.Value).FirstOrDefault(); // лист объектов

      return user;
   }
}
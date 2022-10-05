using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using TestWrapperDMApplication;
using WrapperDM.Helpers;
using WrapperDM.Models;

namespace WrapperDM;

public class WrapperUserController
{
   private const string Path = "api/users";
   
   public async Task<AuthenticateResponse> Authenticate(string login, string password)
   {
      var resClient = new RestClient(UrlHelper.DmApiUrl + Path + "/authenticate");
      var request = new RestRequest();

      var reqModel = new AuthenticateRequest();
      reqModel.Login = login;
      reqModel.Password = password;
      request.AddJsonBody(reqModel);
      
      var response = await resClient.PostAsync(request);
      var deserializedResponse = JsonConvert.DeserializeObject<AuthenticateResponse>(response.Content);
      return deserializedResponse;
   }

   public async Task<List<UserModel>> GetAllUsers(string token)
   {
      var resClient = new RestClient(UrlHelper.DmApiUrl + Path)
         {
            Authenticator = new JwtAuthenticator(token!)
         };
         var request = new RestRequest();
         var response = await resClient.GetAsync(request);
         var deserializedResponse = JsonConvert.DeserializeObject<List<UserModel>>(response.Content);
         return deserializedResponse;
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
      
      var response = await resClient.PostAsync(request);
      var deserializedResponse = JsonConvert.DeserializeObject<bool>(response.Content);
      return deserializedResponse;
   }
   
   public async Task<object> DeleteUser(int userId, string token)
   {
      var resClient = new RestClient(UrlHelper.DmApiUrl + Path + $"?userId ={userId}")
      {
         Authenticator = new JwtAuthenticator(token!)
      };
      var request = new RestRequest();
      request.AddParameter("userId", userId);

      var response = await resClient.DeleteAsync(request);
      return response;
   }
}
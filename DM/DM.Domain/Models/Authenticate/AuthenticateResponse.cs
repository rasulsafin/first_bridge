using DM.DAL.Entities;

namespace DM.Domain.Models
{
    public class AuthenticateResponse
    {
        public string Token { get; set; }

        public AuthenticateResponse(string token)
        {
            Token = token;
        }
    }
}

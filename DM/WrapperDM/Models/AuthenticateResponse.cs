using System;

namespace WrapperDM.Models
{
    public class AuthenticateResponse
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Login { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public string? Role { get; set; }
        
        public long OrganizationId { get; set; }

        public AuthenticateResponse(string id, string name, string login, string email, string role, string token, string organizationId)
        {
            Id = Convert.ToInt64(id);
            Name = name;
            Login = login;
            Email = email;
            Role = role;
            Token = token;
            OrganizationId = Convert.ToInt64(organizationId);
        }
    }
}
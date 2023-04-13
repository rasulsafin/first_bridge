using DM.DAL.Entities;

namespace DM.Domain.Models
{
    public class AuthenticateResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FathersName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string Position { get; set; }
        public string Role { get; set; }
        public long OrganizationId { get; set; }

        public AuthenticateResponse(UserEntity user, string token)
        {
            Id = user.Id;
            Name = user.Name;
            LastName = user.LastName;
            FathersName = user.FathersName;
            Login = user.Login;
            Email = user.Email;
            Role = user.Roles;
            Position = user.Position;
            Token = token;
            Password = user.Password;
            OrganizationId = user.OrganizationId;
        }
    }
}

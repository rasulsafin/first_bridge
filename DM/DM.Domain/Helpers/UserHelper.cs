using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using DM.DAL.Entities;

namespace DM.Domain.Helpers
{
    public static class UserHelper
    {
        public static string GenerateJwtToken(this IConfiguration configuration, UserEntity user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim("roleName", user.Role.Name),
                    new Claim("userName", user.Name),
                    new Claim("lastName", user.LastName),
                    new Claim("fathersName", user.FathersName),
                    new Claim("login", user.Login),
                    new Claim("email", user.Email),
                    new Claim("organizationId", user.OrganizationId.ToString()),
                }),

                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

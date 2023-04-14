using DM.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Principal;
using System.Security.Claims;
using DM.Domain.Implementations;

namespace DM.Domain.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        //   private CurrentUserService _currentUserService;
        //private readonly ILogger _logger;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context, IUserService userService, CurrentUserService _currentUserService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
                AttachUserToContext(context, userService, token, _currentUserService);

            await _next(context);
        }

        public void AttachUserToContext(HttpContext context, IUserService userService, string token, CurrentUserService currentUserService)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                // min 16 characters
                var key = Encoding.ASCII.GetBytes(_configuration["Secret"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                var role = jwtToken.Claims.First(x => x.Type == "roleName").Value;

                var user = userService.GetById(userId);

                var claim = new Claim(ClaimTypes.Name, user.Name);
                var userIdClaim = new Claim(ClaimTypes.NameIdentifier, user.Id.ToString());
                var claimRole = new Claim(ClaimTypes.Role, role.ToString());
                var identity = new ClaimsIdentity(new[] { claim, claimRole/*, userIdClaim*/ }, "jwt");
                var principal = new ClaimsPrincipal(identity);

                context.User = principal;
                currentUserService.SetCurrentUser(userId);
            }
            //TODO: Add catch exceptions
            catch
            {
                // todo: need to add logger
            }
        }
        //eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJyb2xlTmFtZSI6Ik93bmVyIiwibmJmIjoxNjgxNDYxMTU3LCJleHAiOjE2ODIwNjU5NTcsI
        //mlhdCI6MTY4MTQ2MTE1N30.tRH4N-2EfYSM9SBBzCLlTTVPzquWP0bbcTlLuypUd-g

        //eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJyb2xlTmFtZSI6Ik93bmVyIiwibmJmIjoxNjgxNDYxMTU3LCJleHAiOjE2ODIwNjU5NTcsI
        //mlhdCI6MTY4MTQ2MTE1N30.tRH4N-2EfYSM9SBBzCLlTTVPzquWP0bbcTlLuypUd-g
    }
}

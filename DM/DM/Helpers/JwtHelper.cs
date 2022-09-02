using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace DM.Helpers
{
    public static class JwtHelper
    {
        public static long GetUserId(this HttpContext context)
        {
            return int.Parse(context.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}
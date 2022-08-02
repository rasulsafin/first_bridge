using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DM.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }
        private string[] _roles;

        public AuthorizeAttribute(string roles = "")
        {
            Roles = roles;
            _roles = roles.Split(' ');
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user == null)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            if (!_roles.Any(x => user.IsInRole(x)))
            {
                context.Result = new JsonResult(new { message = "Access Denied" }) { StatusCode = StatusCodes.Status403Forbidden };
            }
        }
    }
}

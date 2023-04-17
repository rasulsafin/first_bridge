using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Helpers;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/permission")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [Authorize(new string[] { RoleConst.Admin, RoleConst.Owner })]
        [HttpGet]
        public async Task<IActionResult> GetAllByRole(long roleId)
        {
            var permissions = await _permissionService.GetAllByRole(roleId);

            if (permissions == null)
            {
                return NotFound();
            }

            return Ok(permissions);
        }

        [Authorize(new string[] { RoleConst.Admin, RoleConst.Owner })]
        [HttpPut]
        public async Task<IActionResult> UpdatePermissionOnRole(PermissionModel permissionModel)
        {
            var result = await _permissionService.UpdatePermissionOnRole(permissionModel);

            if (!result) return BadRequest("something went wrong");

            return Ok(result);
        }
    }
}

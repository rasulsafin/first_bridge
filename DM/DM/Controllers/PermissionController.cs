using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [Authorize(RoleConst.SuperAdmin)]
        [HttpGet]
        public async Task<IActionResult> GetAllPermissionsOfUser(long userId)
        {
            var permissions = await _permissionService.GetAllPermissionsOfUser(userId);

            return Ok(permissions);
        }

        [Authorize(RoleConst.SuperAdmin)]
        [HttpPost]
        public async Task<IActionResult> AddPermissionToUser(PermissionModel permissionModel)
        {
            var permissions = await _permissionService.AddPermissionToUser(permissionModel);

            return Ok(true);
        }

        [Authorize(RoleConst.SuperAdmin)]
        [HttpDelete]
        public async Task<IActionResult> DeletePermissionOfUser(PermissionModel permissionModel)
        {
            var result = await _permissionService.RemovePermissionFromUser(permissionModel);

            return Ok(result);
        }
    }
}

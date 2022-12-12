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

        [Authorize(RoleConst.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAllPermissionsOfUser(long userId)
        {
            var permissions = await _permissionService.GetAllPermissionsOfUser(userId);

            if (permissions == null)
            {
                return NotFound();
            }

            return Ok(permissions);
        }

        [Authorize(RoleConst.Admin)]
        [HttpPost]
        public async Task<IActionResult> AddPermissionToUserOrUpdateIfExist(PermissionModel permissionModel)
        {
            if (permissionModel == null) return BadRequest("Invalid Request");
            var permissions = await _permissionService.AddPermissionToUser(permissionModel);
            if (permissions == false) return BadRequest("No Such User Here");

            return Ok(true);
        }

        [Authorize(RoleConst.Admin)]
        [HttpDelete]
        public async Task<IActionResult> DeletePermissionOfUser(PermissionModel permissionModel)
        {
            var result = await _permissionService.RemovePermissionFromUser(permissionModel);

            if (result == false) return BadRequest("something went wrong");

            return Ok(result);
        }
    }
}

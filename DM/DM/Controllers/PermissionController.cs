using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Domain.Implementations;
using DM.Domain.Helpers;

using DM.DAL;
using DM.DAL.Enums;
using DM.DAL.Entities;

using DM.Helpers;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/permission")]
    public class PermissionController : ControllerBase
    {
        private readonly DmDbContext _context;
        private readonly UserEntity _currentUser;

        private readonly IPermissionService _permissionService;
        private readonly ILogger<PermissionService> _logger;

        public PermissionController(DmDbContext context, CurrentUserService currentUserService, IPermissionService permissionService, ILogger<PermissionService> logger)
        {
            _context = context;
            _currentUser = currentUserService.CurrentUser;
            _permissionService = permissionService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllByRole(long roleId)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForRead(_context, _currentUser, PermissionType.Role);

            if (!permission) return StatusCode(403);

            var permissions = await _permissionService.GetAllByRole(roleId);

            if (permissions == null)
            {
                return NotFound();
            }

            return Ok(permissions);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdatePermissionOnRole(PermissionModel permissionModel)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForUpdate(_context, _currentUser, PermissionType.Role);

            if (!permission) return StatusCode(403);

            var result = await _permissionService.UpdatePermissionOnRole(permissionModel);

            if (!result) return BadRequest("something went wrong");

            return Ok(result);
        }
    }
}

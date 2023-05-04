using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Domain.Helpers;
using DM.Domain.Services;
using DM.Domain.Infrastructure.Exceptions;

using DM.DAL;

using DM.Common.Enums;

using DM.Validators.Attributes;

using static DM.Validators.ServiceResponsesValidator;

namespace DM.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/permission")]
    public class PermissionController : ControllerBase
    {
        private readonly DmDbContext _context;
        private readonly UserDto _currentUser;

        private readonly IPermissionService _permissionService;
        private readonly ILogger<PermissionService> _logger;

        public PermissionController(DmDbContext context, CurrentUserService currentUserService, IPermissionService permissionService, ILogger<PermissionService> logger)
        {
            _context = context;
            _currentUser = currentUserService.CurrentUser;
            _permissionService = permissionService;
            _logger = logger;
        }

        /// <summary>
        /// Get permissions by role id.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>List of permissions.</returns>
        /// <response code="200">Permissions list fetched.</response>
        /// <response code="400">Invalid id.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">Could not find permissions.</response>
        /// <response code="500">Something went wrong while fetching the permissions.</response>
        [HttpGet]
        public async Task<IActionResult> GetAllByRole(long roleId)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForRead(_context, _currentUser, PermissionEnum.Role);

                if (!permission) return StatusCode(403);

                var permissions = await _permissionService.GetAllByRole(roleId);

                if (permissions == null) return NotFound();

                return Ok(permissions);
            }
            catch (ANotFoundException ex)
            {
                return CreateProblemResult(this, 404, ex.Message);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        /// <summary>
        /// Updating an existing permission.
        /// </summary>
        /// <param name="permissionModel"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Permission updated.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while updating updated.</response>
        [HttpPut]
        public async Task<IActionResult> UpdatePermissionOnRole(PermissionDto permissionModel)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForUpdate(_context, _currentUser, PermissionEnum.Role);

                if (!permission) return StatusCode(403);

                var result = await _permissionService.UpdatePermissionOnRole(permissionModel);

                if (!result) return BadRequest("something went wrong");

                return Ok(result);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }
    }
}

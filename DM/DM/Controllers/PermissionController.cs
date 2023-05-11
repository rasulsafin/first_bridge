using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using DM.Domain.Interfaces;
using DM.Domain.DTO;
using DM.Domain.Services;
using DM.Domain.Infrastructure.Exceptions;

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
        private readonly UserDto _currentUser;
        private readonly IPermissionService _permissionService;

        public PermissionController(CurrentUserService currentUserService, IPermissionService permissionService)
        {
            _currentUser = currentUserService.CurrentUser;
            _permissionService = permissionService;
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
                var permission = await _permissionService.GetAccess(_currentUser.RoleId, ActionEnum.Read);
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
        /// <param name="permissionDto"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Permission updated.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while updating updated.</response>
        [HttpPut]
        public async Task<IActionResult> UpdatePermissionOnRole(PermissionDto permissionDto)
        {
            try
            {
                var permission = await _permissionService.GetAccess(_currentUser.RoleId, ActionEnum.Update);
                if (!permission) return StatusCode(403);

                var result = await _permissionService.UpdatePermissionOnRole(permissionDto);

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

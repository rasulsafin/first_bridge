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
    [Route("api/role")]
    public class RoleController : ControllerBase
    {
        private readonly UserDto _currentUser;
        private readonly IRoleService _roleService;

        public RoleController(CurrentUserService currentUserService, IRoleService roleService)
        {
            _currentUser = currentUserService.CurrentUser;
            _roleService = roleService;
        }

        /// <summary>
        /// Get list of all existing roles.
        /// </summary>
        /// <returns>List of roles.</returns>
        /// <response code="200">Roles list fetched.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while fetching the roles.</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var permission = await _roleService.GetAccess(_currentUser.RoleId, ActionEnum.Read);
                if (!permission) return StatusCode(403);

                var roles = await _roleService.GetAll();

                return Ok(roles);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        /// <summary>
        /// Get role by their id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>Role Id</returns>
        /// <response code="200">Role found.</response>
        /// <response code="400">Invalid id.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">Could not find role.</response>
        /// <response code="500">Something went wrong while fetching the role.</response>
        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetById(long roleId)
        {
            try
            {
                var permission = await _roleService.GetAccess(_currentUser.RoleId, ActionEnum.Read);
                if (!permission) return StatusCode(403);

                var role = _roleService.GetById(roleId);

                return Ok(role);
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
        /// Create new role.
        /// </summary>
        /// <param name="roleDto"></param>
        /// <returns>Id of created role.</returns>        
        /// <response code="200">Role created.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while creating new role.</response>
        [HttpPost]
        public async Task<IActionResult> Create(RoleForCreateDto roleDto)
        {
            try
            {
                var permission = await _roleService.GetAccess(_currentUser.RoleId, ActionEnum.Create);
                if (!permission) return StatusCode(403);

                var id = await _roleService.Create(roleDto);

                return Ok(id);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        /// <summary>
        /// Updating an existing role.
        /// </summary>
        /// <param name="roleDto"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Role updated.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while updating role.</response>
        [HttpPut]
        public async Task<IActionResult> Update(RoleForUpdateDto roleDto)
        {
            try
            {
                var permission = await _roleService.GetAccess(_currentUser.RoleId, ActionEnum.Update);
                if (!permission) return StatusCode(403);

                var checker = await _roleService.Update(roleDto);

                return Ok(checker);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        /// <summary>
        /// Delete existing role.
        /// </summary>
        /// <param name="roleId">Id of the role to be deleted.</param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Role was deleted successfully.</response>
        /// <response code="400">Invalid id.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">Role was not found.</response>
        /// <response code="500">Something went wrong while deleting role.</response>
        [HttpDelete]
        public async Task<IActionResult> Delete(int roleId)
        {
            try
            {
                var permission = await _roleService.GetAccess(_currentUser.RoleId, ActionEnum.Delete);
                if (!permission) return StatusCode(403);

                var checker = await _roleService.Delete(roleId);

                return Ok(checker);
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
    }
}

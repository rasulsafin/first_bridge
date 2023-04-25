using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Domain.Helpers;
using DM.Domain.Implementations;
using DM.Domain.Exceptions;

using DM.DAL;
using DM.DAL.Enums;
using DM.DAL.Entities;

using DM.Helpers;

using static DM.Validators.ServiceResponsesValidator;


namespace DM.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/role")]
    public class RoleController : ControllerBase
    {

        private readonly DmDbContext _context;
        private readonly UserModel _currentUser;

        private readonly IRoleService _roleService;
        private readonly ILogger<RoleService> _logger;

        public RoleController(DmDbContext context, CurrentUserService currentUserService, IRoleService roleService, ILogger<RoleService> logger)
        {
            _context = context;
            _currentUser = currentUserService.CurrentUser;
            _roleService = roleService;
            _logger = logger;
        }

        /// <summary>
        /// Get list of all existing roles.
        /// </summary>
        /// <returns>List of roles.</returns>
        /// <response code="200">Roles list fetched.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while fetching the roles.</response>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForRead(_context, _currentUser, PermissionType.Role);

                if (!permission) return StatusCode(403);

                var roles = _roleService.GetAll();

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
        public IActionResult GetById(long roleId)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForRead(_context, _currentUser, PermissionType.Role);

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
        /// <param name="roleModel"></param>
        /// <returns>Id of created role.</returns>        
        /// <response code="200">Role created.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while creating new role.</response>
        [HttpPost]
        public async Task<IActionResult> Create(RoleForCreateModel roleModel)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForCreate(_context, _currentUser, PermissionType.Role);

                if (!permission) return StatusCode(403);

                var id = await _roleService.Create(roleModel);

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
        /// <param name="roleModel"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Role found.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while updating role.</response>
        [HttpPut]
        public async Task<IActionResult> Update(RoleForUpdateModel roleModel)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForUpdate(_context, _currentUser, PermissionType.Role);

                if (!permission) return StatusCode(403);

                var checker = await _roleService.Update(roleModel);

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
                var permission = AuthorizationHelper.CheckUserPermissionsForDelete(_context, _currentUser, PermissionType.Role);

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

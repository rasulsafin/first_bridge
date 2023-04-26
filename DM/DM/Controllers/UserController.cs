using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using DM.Domain.Exceptions;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Domain.Helpers;
using DM.Domain.Implementations;

using DM.DAL;
using DM.DAL.Enums;

using DM.Helpers;

using static DM.Validators.ServiceResponsesValidator;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly DmDbContext _context;
        private readonly UserModel _currentUser;

        private readonly IUserService _userService;
        private readonly IUserProjectService _userProjectService;

        public UserController(DmDbContext context, CurrentUserService currentUserService,
            IUserService userService, IUserProjectService userProjectService)
        {
            _context = context;
            _currentUser = currentUserService.CurrentUser;
            _userService = userService;
            _userProjectService = userProjectService;
        }

        /// <summary>
        /// Getting all existing users.
        /// </summary>
        /// <returns>List of all users.</returns>        
        /// <response code="200">Users list fetched.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while fetching the users.</response>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForRead(_context, _currentUser, PermissionType.User);

                if (!permission) return StatusCode(403);

                var users = await _userService.GetAll();

                return Ok(users);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        /// <summary>
        /// Get user by their id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>User Id.</returns>        
        /// <response code="200">User found.</response>
        /// <response code="400">Invalid id.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">Could not find user.</response>
        /// <response code="500">Something went wrong while fetching the user.</response>
        [HttpGet("{userId}")]
        [Authorize]
        public IActionResult GetById(long userId)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForRead(_context, _currentUser, PermissionType.User);

                if (!permission) return BadRequest(403);

                var user = _userService.GetById(userId);

                return Ok(user);
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
        /// Create new user.
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>Id of created user.</returns>        
        /// <response code="200">User created.</response>
        /// <response code="400">User with the same login already exists OR one/multiple of required values is/are empty.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while creating new user.</response>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(UserForCreateModel userModel)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForCreate(_context, _currentUser, PermissionType.User);

                if (!permission) return BadRequest(403);

                if (userModel == null) return NotFound();

                var id = await _userService.Create(userModel);

                return Ok(id);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest("Organization does not exist:" + ex.Message);
            }
            catch (ArgumentValidationException ex)
            {
                return CreateProblemResult(this, 400, ex.Message);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        /// <summary>
        /// Updating an existing user.
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">User updated.</response>
        /// <response code="400">User with the same login already exists OR one/multiple of required values is/are empty.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while updating user.</response>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(UserForUpdateModel userModel)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForUpdate(_context, _currentUser, PermissionType.User);

                if (!permission) return BadRequest(403);

                var checker = await _userService.Update(userModel);

                return Ok(checker);
            }
            catch (ArgumentValidationException ex)
            {
                return CreateProblemResult(this, 400, ex.Message);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        /// <summary>
        /// Delete existing user.
        /// </summary>
        /// <param name="userId">Id of the user to be deleted.</param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">User was deleted successfully.</response>
        /// <response code="400">Invalid id.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">User was not found.</response>
        /// <response code="500">Something went wrong while deleting user.</response>
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(int userId)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForDelete(_context, _currentUser, PermissionType.User);

                if (!permission) return BadRequest(403);

                var checker = await _userService.Delete(userId);

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

        /// <summary>
        /// Authorization of an existing user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">User found.</response>
        /// <response code="500">Something went wrong when authorizing the user.</response>
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            try
            {
                var response = await _userService.Authenticate(model);

                if (response == null) return BadRequest(new { message = "Username or password is incorrect" });

                return Ok(response);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        /// <summary>
        /// Add user to project.
        /// </summary>
        /// <param name="userProjectModel"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">User added.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">User or project was not found.</response>
        /// <response code="500">Something went wrong when adding to the project.</response>
        [HttpPost("addToProject")]
        [Authorize]
        public async Task<IActionResult> AddToProject(UserProjectModel userProjectModel)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForCreate(_context, _currentUser, PermissionType.User);

                if (!permission) return BadRequest(403);

                var checker = await _userProjectService.AddToProject(userProjectModel);

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

        /// <summary>
        /// Adding a list of projects to the user.
        /// </summary>
        /// <param name="userProjectModel"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Users added.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">User or project was not found.</response>
        /// <response code="500">Something went wrong when adding to the project.</response>
        [HttpPost("addProjectListToUser")]
        [Authorize]
        public async Task<IActionResult> AddToProjects(List<UserProjectModel> userProjectModel)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForCreate(_context, _currentUser, PermissionType.User);

                if (!permission) return BadRequest(403);

                var checker = await _userProjectService.AddToProjects(userProjectModel);

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

        /// <summary>
        /// Deleting a project from a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="projectId"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Project deleted.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">UserProject not found</response>
        /// <response code="500">Something went wrong when deleting the project.</response>
        [HttpDelete("deleteProjectFromUser")]
        [Authorize]
        public async Task<IActionResult> DeleteFromProject(long userId, long projectId)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForDelete(_context, _currentUser, PermissionType.User);

                if (!permission) return BadRequest(403);

                var checker = await _userProjectService.DeleteFromProject(userId, projectId);

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

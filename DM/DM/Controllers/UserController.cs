using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

using DM.Domain.Exceptions;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Domain.Helpers;
using DM.Domain.Implementations;

using DM.DAL;
using DM.DAL.Entities;

using DM.Helpers;

using static DM.Validators.ServiceResponsesValidator;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly DmDbContext _context;
        private readonly UserEntity _currentUser;

        private readonly IUserService _userService;
        private readonly IUserProjectService _userProjectService;
        private readonly ILogger<UserService> _logger;

        public UsersController(DmDbContext context, CurrentUserService currentUserService,
            IUserService userService, IUserProjectService userProjectService, ILogger<UserService> logger)
        {
            _context = context;
            _currentUser = currentUserService.CurrentUser;
            _userService = userService;
            _userProjectService = userProjectService;
            _logger = logger;
        }

        /// <summary>
        /// Get list of all existing users
        /// </summary>
        /// <returns>List of users.</returns>
        /// <response code="200">Returns found list of users.</response>
        /// <response code="500">Something went wrong while retrieving the users.</response>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForRead(_context, _currentUser, PermissionType.User);

                if (!permission) return BadRequest("Access Denied");

                var users = await _userService.GetAll();
                return Ok(users);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        /// <summary>
        /// Get user by their id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>User Id</returns>
        /// <response code="200">User found.</response>
        /// <response code="400">Invalid id.</response>
        /// <response code="404">Could not find user.</response>
        /// <response code="500">Something went wrong while retrieving the user.</response>
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
        /// Create new user
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>Id of created user</returns>
        /// <response code="400">User with the same login already exists OR one/multiple of required values is/are empty.</response>
        /// <response code="500">Something went wrong while creating new user.</response>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(UserForCreateModel userModel)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForCreate(_context, _currentUser, PermissionType.User);

            if (!permission) return BadRequest(403);

            if (userModel == null)
            {
                return BadRequest("Invalid Request");
            }

            try
            {
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

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(UserForUpdateModel user)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForUpdate(_context, _currentUser, PermissionType.User);

                if (!permission) return BadRequest(403);

                var checker = await _userService.Update(user);
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
        /// <param name="userID">Id of the user to be deleted.</param>
        /// <returns>True if user is deleted.</returns>
        /// <response code="200">User was deleted successfully.</response>
        /// <response code="400">Invalid id.</response>
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

                await _userService.Delete(userId);
                return Ok();
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

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

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

        [HttpPost("addListToProject")]
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

        [HttpDelete("deleteFromProject")]
        [Authorize]
        public async Task<IActionResult> DeleteFromProject(long userProjectId)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForDelete(_context, _currentUser, PermissionType.User);

                if (!permission) return BadRequest(403);

                var checker = await _userProjectService.DeleteFromProject(userProjectId);
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

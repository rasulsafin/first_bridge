using System;
using System.Threading.Tasks;
using System.Collections.Generic;

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
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly UserDto _currentUser;
        private readonly IUserService _userService;
        private readonly IUserProjectService _userProjectService;

        public UserController(CurrentUserService currentUserService,
            IUserService userService, IUserProjectService userProjectService)
        {
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
                var permission = await _userService.GetAccess(_currentUser.RoleId, ActionEnum.Read);

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
        public async Task<IActionResult> GetById(long userId)
        {
            try
            {
                var permission = await _userService.GetAccess(_currentUser.RoleId, ActionEnum.Read);

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
        /// <param name="userDto"></param>
        /// <returns>Id of created user.</returns>        
        /// <response code="200">User created.</response>
        /// <response code="400">User with the same login already exists OR one/multiple of required values is/are empty.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while creating new user.</response>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(UserForCreateDto userDto)
        {
            try
            {
                var permission = await _userService.GetAccess(_currentUser.RoleId, ActionEnum.Create);

                if (!permission) return BadRequest(403);

                if (userDto == null) return NotFound();

                var id = await _userService.Create(userDto);

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
        /// <param name="userDto"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">User updated.</response>
        /// <response code="400">User with the same login already exists OR one/multiple of required values is/are empty.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while updating user.</response>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(UserForUpdateDto userDto)
        {
            try
            {
                var permission = await _userService.GetAccess(_currentUser.RoleId, ActionEnum.Update);

                if (!permission) return BadRequest(403);

                var checker = await _userService.Update(userDto);

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
                var permission = await _userService.GetAccess(_currentUser.RoleId, ActionEnum.Delete);

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
        /// <param name="Dto"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">User found.</response>
        /// <response code="500">Something went wrong when authorizing the user.</response>
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest Dto)
        {
            try
            {
                var response = await _userService.Authenticate(Dto);

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
        /// <param name="userProjectDto"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">User added.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">User or project was not found.</response>
        /// <response code="500">Something went wrong when adding to the project.</response>
        [HttpPost("addToProject")]
        [Authorize]
        public async Task<IActionResult> AddToProject(UserProjectDto userProjectDto)
        {
            try
            {
                var permission = await _userService.GetAccess(_currentUser.RoleId, ActionEnum.Create);

                if (!permission) return BadRequest(403);

                var checker = await _userProjectService.AddToProject(userProjectDto);

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
        /// <param name="userProjectDto"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Users added.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">User or project was not found.</response>
        /// <response code="500">Something went wrong when adding to the project.</response>
        [HttpPost("addProjectListToUser")]
        [Authorize]
        public async Task<IActionResult> AddToProjects(List<UserProjectDto> userProjectDto)
        {
            try
            {
                var permission = await _userService.GetAccess(_currentUser.RoleId, ActionEnum.Create);

                if (!permission) return BadRequest(403);

                var checker = await _userProjectService.AddToProjects(userProjectDto);

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
                var permission = await _userService.GetAccess(_currentUser.RoleId, ActionEnum.Delete);

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

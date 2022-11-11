using DM.Domain.Exceptions;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static DM.Validators.ServiceResponsesValidator;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get list of all existing users
        /// </summary>
        /// <returns>List of users.</returns>
        /// <response code="200">Returns found list of users.</response>
        /// <response code="500">Something went wrong while retrieving the users.</response>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var users = _userService.GetAll();
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
        public IActionResult GetById(long userId)
        {
            try 
            {
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
        // [Authorize]
        public async Task<IActionResult> Create(UserModel userModel)
        {
            if (userModel == null)
            {
                return BadRequest("Invalid Request");
            }
            if (!RoleConst.SuperAdmin.Contains(userModel.Roles))
            {
                return BadRequest("The Role does not exist");
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
        public async Task<IActionResult> Update(UserModelForUpdate user)
        {
            try
            {
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
       // [Authorize(RoleConst.UserAdmin)]
        public async Task<IActionResult> Delete(int userId)
        {
            try
            {
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
    }
}

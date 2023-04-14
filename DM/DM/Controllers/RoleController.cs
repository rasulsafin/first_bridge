using DM.Domain.Exceptions;
using DM.Domain.Implementations;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/role")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService listFieldService)
        {
            _roleService = listFieldService;
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
            var roles = _roleService.GetAll();
            return Ok(roles);
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
        [HttpGet("{roleId}")]
        public IActionResult GetById(long roleId)
        {
            try
            {
                var user = _roleService.GetById(roleId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound();
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
        public async Task<IActionResult> Create(RoleModel roleModel)
        {
            if (roleModel == null)
            {
                return BadRequest("Invalid Request");
            }

            try
            {
                var id = await _roleService.Create(roleModel);
                return Ok(id);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest("Organization does not exist:" + ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(RoleModel roleModel)
        {
            try
            {
                var checker = await _roleService.Update(roleModel);
                return Ok(checker);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest("Organization does not exist:" + ex.Message);
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
        public async Task<IActionResult> Delete(int roleId)
        {
            try
            {
                await _roleService.Delete(roleId);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest("Organization does not exist:" + ex.Message);
            }
        }
    }
}

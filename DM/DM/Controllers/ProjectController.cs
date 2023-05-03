﻿using System.Threading.Tasks;
using System.Collections.Generic;

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
    [Route("api/project")]
    public class ProjectController : ControllerBase
    {
        private readonly DmDbContext _context;
        private readonly UserDto _currentUser;

        private readonly IProjectService _projectService;
        private readonly IUserProjectService _userProjectService;
        private readonly ILogger<ProjectService> _logger;

        public ProjectController(DmDbContext context, CurrentUserService currentUserService, IProjectService projectService,
            IUserProjectService userProjectService, ILogger<ProjectService> logger)
        {
            _context = context;
            _currentUser = currentUserService.CurrentUser;
            _projectService = projectService;
            _userProjectService = userProjectService;
            _logger = logger;
        }

        /// <summary>
        /// Get list of all existing projects.
        /// </summary>
        /// <returns>List of projects.</returns>
        /// <response code="200">Projects list fetched.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while fetching the projects.</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForRead(_context, _currentUser, PermissionEnum.Project);

                if (!permission) return StatusCode(403);

                var projects = await _projectService.GetAll();

                return Ok(projects);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        /// <summary>
        /// Get project by their id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>Project Id</returns>
        /// <response code="200">Project found.</response>
        /// <response code="400">Invalid id.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">Could not find project.</response>
        /// <response code="500">Something went wrong while fetching the project.</response>
        [HttpGet("{projectId}")]
        public IActionResult GetById(long projectId)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForRead(_context, _currentUser, PermissionEnum.Project);

                if (!permission) return StatusCode(403);

                var project = _projectService.GetById(projectId);

                if (project == null)
                    return NotFound();

                return Ok(project);
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
        /// Create new project.
        /// </summary>
        /// <param name="projectModel"></param>
        /// <returns>Id of created project.</returns>        
        /// <response code="200">Project created.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while creating new project.</response>
        [HttpPost]
        public async Task<IActionResult> Create(ProjectForReadDto projectModel)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForCreate(_context, _currentUser, PermissionEnum.Project);

                if (!permission) return StatusCode(403);

                var id = await _projectService.Create(projectModel);

                return Ok(id);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        /// <summary>
        /// Updating an existing project.
        /// </summary>
        /// <param name="projectModel"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Project updated.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while updating project.</response>
        [HttpPut]
        public async Task<IActionResult> Update(ProjectForUpdateDto projectModel)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForUpdate(_context, _currentUser, PermissionEnum.Project);

                if (!permission) return StatusCode(403);

                var checker = await _projectService.Update(projectModel);

                if (!checker) return BadRequest();

                return Ok(checker);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        /// <summary>
        /// Delete existing project.
        /// </summary>
        /// <param name="projectId">Id of the project to be deleted.</param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Project was deleted successfully.</response>
        /// <response code="400">Invalid id.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">Project was not found.</response>
        /// <response code="500">Something went wrong while deleting project.</response>
        [HttpDelete]
        public async Task<IActionResult> Archive(long projectId)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForDelete(_context, _currentUser, PermissionEnum.Project);

            if (!permission) return StatusCode(403);

            var checker = await _projectService.Archive(projectId);

            if (!checker) return NotFound();

            return Ok(checker);
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
        public async Task<IActionResult> AddToProject(UserProjectDto userProjectModel)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForCreate(_context, _currentUser, PermissionEnum.User);

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
        /// Adding a list of users to the project.
        /// </summary>
        /// <param name="userProjectModel"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Users added.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">User or project was not found.</response>
        /// <response code="500">Something went wrong when adding to the project.</response>
        [HttpPost("addUserListToProject")]
        public async Task<IActionResult> AddToProjects(List<UserProjectDto> userProjectModel)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForCreate(_context, _currentUser, PermissionEnum.User);

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
        /// Deleting a user from a project.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="projectId"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Project deleted.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">UserProject not found</response>
        /// <response code="500">Something went wrong when deleting the user.</response>
        [HttpDelete("deleteUserFromProject")]
        public async Task<IActionResult> DeleteFromProject(long userId, long projectId)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForDelete(_context, _currentUser, PermissionEnum.User);

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

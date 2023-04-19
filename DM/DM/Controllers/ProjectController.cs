using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DM.Domain.Helpers;
using DM.Domain.Implementations;
using DM.Domain.Interfaces;
using DM.Domain.Models;

using DM.DAL.Entities;
using DM.DAL;

using DM.Helpers;
using DM.Domain.Exceptions;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/project")]
    public class ProjectController : ControllerBase
    {
        private readonly DmDbContext _context;
        private readonly UserEntity _currentUser;

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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForRead(_context, _currentUser, PermissionType.Project);

            if (!permission) return StatusCode(403);

            var projects = await _projectService.GetAll();

            return Ok(projects);
        }

        [HttpGet("{projectId}")]
        [Authorize]
        public async Task<IActionResult> GetById(long projectId)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForRead(_context, _currentUser, PermissionType.Project);

            if (!permission) return StatusCode(403);

            var project = await _projectService.GetById(projectId);

            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ProjectModel projectModel)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForCreate(_context, _currentUser, PermissionType.Project);

            if (!permission) return StatusCode(403);

            var id = await _projectService.Create(projectModel);

            return Ok(id);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(ProjectModel projectModel)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForUpdate(_context, _currentUser, PermissionType.Project);

            if (!permission) return StatusCode(403);

            var checker = await _projectService.Update(projectModel);

            if (!checker) return BadRequest("This model does not exist");

            return Ok(checker);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(long projectId)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForDelete(_context, _currentUser, PermissionType.Project);

            if (!permission) return StatusCode(403);

            var checker = await _projectService.Delete(projectId);

            if (!checker) return NotFound();

            return Ok(checker);
        }

        [HttpPost("addToProject")]
        [Authorize]
        public async Task<IActionResult> AddToProject(UserProjectModel userProjectModel)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForCreate(_context, _currentUser, PermissionType.User);

                if (!permission) return BadRequest("Access Denied");

                var checker = await _userProjectService.AddToProject(userProjectModel);
                return Ok(checker);
            }
            catch (ANotFoundException ex)
            {
                return BadRequest("ANotFoundException");
            }
        }


        [HttpPost("addUserListToProject")]
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
                return BadRequest("ANotFoundException");
            }
        }

        [HttpDelete("deleteUserFromProject")]
        [Authorize]
        public async Task<IActionResult> DeleteFromProject(long userProjectId)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForDelete(_context, _currentUser, PermissionType.User);

                if (!permission) return BadRequest("Access Denied");

                var checker = await _userProjectService.DeleteFromProject(userProjectId);
                return Ok(checker);
            }
            catch (ANotFoundException ex)
            {
                return BadRequest("ANotFoundException");
            }
        }
    }
}

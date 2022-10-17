using DM.DAL.Entities;
using DM.Domain.Helpers;
using DM.Domain.Implementations;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DM.DAL;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/project")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly DmDbContext _context;
        private readonly UserEntity _currentUser;

        public ProjectController(IProjectService projectService, DmDbContext context, CurrentUserService userService)
        {
            _projectService = projectService;
            _context = context;
            _currentUser = userService.CurrentUser;
        }

        [Authorize(RoleConst.SuperAdmin)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // логика проверки доступа для GetAll перенесена в сервис
            var projects = await _projectService.GetAll();

            return Ok(projects);
        }

        [Authorize(RoleConst.UserAdmin)]
        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetById(long projectId)
        {
            var permission = AuthorizationHelper.CheckUsersPermissionsById(_context, _currentUser, PermissionType.Project, projectId);

            if (permission == null || !permission.Read)
            {
                return StatusCode(403);
            }

            var project = await _projectService.GetById(projectId);
            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [Authorize(RoleConst.UserAdmin)]
        [HttpPost]
        public async Task<IActionResult> Create(ProjectModel projectModel)
        {
            var permission = AuthorizationHelper.CheckUsersPermissionsForCreate(_context, _currentUser, PermissionType.Project);

            if (permission == null)
            {
                return StatusCode(403);
            }

            var id = await _projectService.Create(projectModel);

            return Ok(id);
        }
        
        [Authorize(RoleConst.UserAdmin)]
        [HttpPut]
        public async Task<IActionResult> Update(ProjectModel projectModel)
        {
            var permission = AuthorizationHelper.CheckUsersPermissionsForUpdate(_context, _currentUser, PermissionType.Project, projectModel.Id);

            if (permission == null)
            {
                return StatusCode(403);
            }

            var checker = await _projectService.Update(projectModel);

            if (checker == false)
            {
                return BadRequest("the fields must not contain invalid characters");
            }

            return Ok(checker);
        }


        [Authorize(RoleConst.UserAdmin)]
        [HttpDelete]
        public async Task<IActionResult> Delete(long projectId)
        {
            var permission = AuthorizationHelper.CheckUsersPermissionsForDelete(_context, _currentUser, PermissionType.Record, projectId);

            if (permission == null)
            {
                return StatusCode(403);
            }

            var checker = await _projectService.Delete(projectId);

            if (checker == false)
            {
                return NotFound();
            }

            return Ok(checker);
        }
    }
}

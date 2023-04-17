using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using DM.Domain.Helpers;
using DM.Domain.Implementations;
using DM.Domain.Interfaces;
using DM.Domain.Models;

using DM.DAL.Entities;
using DM.DAL;

using DM.Helpers;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/project")]
    public class ProjectController : ControllerBase
    {
        private readonly DmDbContext _context;
        private readonly UserEntity _currentUser;

        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService, DmDbContext context, CurrentUserService userService)
        {
            _projectService = projectService;
            _context = context;
            _currentUser = userService.CurrentUser;
        }

        [Authorize(new string[] { RoleConst.Admin, RoleConst.Owner })]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _projectService.GetAll();

            return Ok(projects);
        }

        [Authorize(new string[] { RoleConst.Admin, RoleConst.Owner })]
        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetById(long projectId)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsById(_context, _currentUser, PermissionType.Project);

            if (!permission)
            {
                return StatusCode(403);
            }

            var project = await _projectService.GetById(projectId);

            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [Authorize(new string[] { RoleConst.Admin, RoleConst.Owner })]
        [HttpPost]
        public async Task<IActionResult> Create(ProjectModel projectModel)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForCreate(_context, _currentUser, PermissionType.Project);

            if (!permission)
            {
                return StatusCode(403);
            }

            var id = await _projectService.Create(projectModel);

            return Ok(id);
        }

        [Authorize(new string[] { RoleConst.Admin, RoleConst.Owner })]
        [HttpPut]
        public async Task<IActionResult> Update(ProjectModel projectModel)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForUpdate(_context, _currentUser, PermissionType.Project);

            if (!permission)
            {
                return StatusCode(403);
            }

            var checker = await _projectService.Update(projectModel);

            if (!checker)
            {
                return BadRequest("the fields must not contain invalid characters");
            }

            return Ok(checker);
        }


        [Authorize(new string[] { RoleConst.Admin, RoleConst.Owner })]
        [HttpDelete]
        public async Task<IActionResult> Delete(long projectId)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForDelete(_context, _currentUser, PermissionType.Record);

            if (!permission)
            {
                return StatusCode(403);
            }

            var checker = await _projectService.Delete(projectId);

            if (!checker)
            {
                return NotFound();
            }

            return Ok(checker);
        }
    }
}

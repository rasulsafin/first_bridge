using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/project")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [Authorize(RoleConst.UserAdmin)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _projectService.GetAll();

            return Ok(projects);
        }

        [Authorize(RoleConst.UserAdmin)]
        [HttpGet("{projectId}")]
        public IActionResult GetById(long projectId)
        {
            var project = _projectService.GetById(projectId);
            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [Authorize(RoleConst.UserAdmin)]
        [HttpPost]
        public async Task<IActionResult> Create(ProjectModel projectModel)
        {
            var id = await _projectService.Create(projectModel);

            return Ok(id);
        }

        [Authorize(RoleConst.UserAdmin)]
        [HttpGet("{projectIdForTemplate}/template")]
        public async Task<IActionResult> GetProjectTemplateOfRecord(long projectIdForTemplate)
        {
            var jsonDoc = await _projectService.GetProjectTemplateOfRecord(projectIdForTemplate);
            var rootElement = jsonDoc.RootElement.ToString();

            return Ok(rootElement);
        }
    }
}

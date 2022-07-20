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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _projectService.GetAll();

            return Ok(projects);
        }

        [HttpGet("{projectId}")]
        public IActionResult GetById(long projectId)
        {
            var project = _projectService.GetById(projectId);

            return Ok(project);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(ProjectModel projectModel)
        {
            var id = await _projectService.Create(projectModel);

            return Ok(id);
        }
    }
}

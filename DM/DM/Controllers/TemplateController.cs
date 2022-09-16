using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/template")]
    public class TemplateController : ControllerBase
    {
        private readonly ITemplateService _templateService;

        public TemplateController(ITemplateService templateService)
        {
            _templateService = templateService;
        }


        [Authorize(RoleConst.UserAdmin)]
        [HttpGet()]
        public async Task<IActionResult> GetProjectTemplateOfRecord(long projectId)
        {
            var templates = await _templateService.GetTemplatesOfProject(projectId);

            if (templates == null) return NotFound();

            return Ok(templates);
        }

        [Authorize(RoleConst.UserAdmin)]
        [HttpPost()]
        public IActionResult AddTemplateToProject(TemplateModel templateModel)
        {
            if (templateModel == null) return BadRequest("Invalid Request");
            var template = _templateService.AddTemplateToProject(templateModel);
            return Ok(template);
        }

        [Authorize(RoleConst.UserAdmin)]
        [HttpPut()]
        public IActionResult EditExistingTemplateOfProject(TemplateModelForEdit templateModelForEdit)
        {
            var template = _templateService.EditExistingTemplateOfProject(templateModelForEdit);
            return Ok(template);
        }
    }
}

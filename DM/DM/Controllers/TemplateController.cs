using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Helpers;

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


        [Authorize(new string[] { RoleConst.Admin, RoleConst.Owner })]
        [HttpGet()]
        public async Task<IActionResult> GetProjectTemplateOfRecord(long projectId)
        {
            var templates = await _templateService.GetTemplatesOfProject(projectId);

            if (templates == null) return NotFound();

            return Ok(templates);
        }

        [Authorize(new string[] { RoleConst.Admin, RoleConst.Owner })]
        [HttpPost()]
        public IActionResult AddTemplateToProject(TemplateModel templateModel)
        {
            if (templateModel == null) return BadRequest("Invalid Request");

            var template = _templateService.Create(templateModel);

            return Ok(template);
        }

        [Authorize(new string[] { RoleConst.Admin, RoleConst.Owner })]
        [HttpPut()]
        public IActionResult EditExistingTemplateOfProject(TemplateModelForEdit templateModelForEdit)
        {
            var template = _templateService.Update(templateModelForEdit);

            return Ok(template);
        }
    }
}

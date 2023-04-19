using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DM.Domain.Interfaces;
using DM.Domain.Implementations;
using DM.Domain.Helpers;

using DM.DAL;
using DM.DAL.Entities;

using DM.Helpers;
using DM.Domain.Models;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/template")]
    public class TemplateController : ControllerBase
    {
        private readonly DmDbContext _context;
        private readonly UserEntity _currentUser;

        private readonly ITemplateService _templateService;
        private readonly ILogger<TemplateService> _logger;

        public TemplateController(DmDbContext context, CurrentUserService currentUserService, ITemplateService templateService, ILogger<TemplateService> logger)
        {
            _context = context;
            _currentUser = currentUserService.CurrentUser;
            _templateService = templateService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetProjectTemplateOfRecord(long projectId)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForRead(_context, _currentUser, PermissionType.Template);

            if (!permission) return StatusCode(403);

            var templates = await _templateService.GetAllOfProject(projectId);

            if (templates == null) return NotFound();

            return Ok(templates);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddTemplateToProject(TemplateModel templateModel)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForCreate(_context, _currentUser, PermissionType.Template);

            if (!permission) return StatusCode(403);

            if (templateModel == null) return BadRequest("Invalid Request");

            var template = _templateService.Create(templateModel);

            return Ok(template);
        }

        [HttpPut]
        [Authorize]
        public IActionResult EditExistingTemplateOfProject(TemplateForUpdateModel templateModelForEdit)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForUpdate(_context, _currentUser, PermissionType.Template);

            if (!permission) return StatusCode(403);

            var template = _templateService.Update(templateModelForEdit);

            return Ok(template);
        }
    }
}

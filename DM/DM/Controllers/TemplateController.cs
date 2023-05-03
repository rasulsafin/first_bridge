using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DM.Domain.Services;
using DM.Domain.Helpers;
using DM.Domain.Models;
using DM.Domain.Interfaces;

using DM.DAL;
using DM.DAL.Enums;

using DM.Helpers;

using static DM.Validators.ServiceResponsesValidator;
using DM.Domain.Infrastructure.Exceptions;

namespace DM.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/template")]
    public class TemplateController : ControllerBase
    {
        private readonly DmDbContext _context;
        private readonly UserDto _currentUser;

        private readonly ITemplateService _templateService;
        private readonly ILogger<TemplateService> _logger;

        public TemplateController(DmDbContext context, CurrentUserService currentUserService,
            ITemplateService templateService, ILogger<TemplateService> logger)
        {
            _context = context;
            _currentUser = currentUserService.CurrentUser;
            _templateService = templateService;
            _logger = logger;
        }

        /// <summary>
        /// Getting all templates of one project.
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        /// <returns>List of templates.</returns>        
        /// <response code="200">Templates list fetched.</response>
        /// <response code="400">Invalid project id.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">Templates was not found.</response>
        /// <response code="500">Something went wrong while fetching templates.</response>
        [HttpGet]
        public async Task<IActionResult> GetProjectTemplateOfRecord(long projectId)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForRead(_context, _currentUser, PermissionEnum.Template);

                if (!permission) return StatusCode(403);

                var templates = await _templateService.GetAllOfProject(projectId);

                if (templates == null) return NotFound();

                return Ok(templates);
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
        /// Create new template.
        /// </summary>
        /// <param name="templateForCreateModel"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Template created.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while adding template.</response>
        [HttpPost]
        public async Task<IActionResult> AddTemplateToProject(TemplateForCreateDto templateForCreateModel)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForCreate(_context, _currentUser, PermissionEnum.Template);

                if (!permission) return StatusCode(403);

                if (templateForCreateModel == null) return NotFound();

                var template = await _templateService.Create(templateForCreateModel);

                return Ok(template);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        /// <summary>
        /// Updating an existing template.
        /// </summary>
        /// <param name="templateModelForEdit"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Template updated.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">Template not found.</response>
        /// <response code="500">Something went wrong when updating the template.</response>
        [HttpPut]
        public async Task<IActionResult> EditExistingTemplateOfProject(TemplateForUpdateDto templateModelForEdit)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForUpdate(_context, _currentUser, PermissionEnum.Template);

                if (!permission) return StatusCode(403);

                var checker = await _templateService.Update(templateModelForEdit);

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

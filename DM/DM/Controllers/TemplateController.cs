using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using DM.Domain.Services;
using DM.Domain.DTO;
using DM.Domain.Interfaces;
using DM.Domain.Infrastructure.Exceptions;

using DM.Common.Enums;

using DM.Validators.Attributes;

using static DM.Validators.ServiceResponsesValidator;

namespace DM.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/template")]
    public class TemplateController : ControllerBase
    {
        private readonly UserDto _currentUser;
        private readonly ITemplateService _templateService;

        public TemplateController(CurrentUserService currentUserService, ITemplateService templateService)
        {
            _currentUser = currentUserService.CurrentUser;
            _templateService = templateService;
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
                var permission = await _templateService.GetAccess(_currentUser.RoleId, ActionEnum.Read);
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
        /// Get template by their id.
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns>Template Id.</returns>        
        /// <response code="200">Template found.</response>
        /// <response code="400">Invalid id.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">Could not find template.</response>
        /// <response code="500">Something went wrong while fetching the template.</response>
        [HttpGet("{templateId}")]
        [Authorize]
        public async Task<IActionResult> GetById(long templateId)
        {
            try
            {
                var permission = await _templateService.GetAccess(_currentUser.RoleId, ActionEnum.Read);
                if (!permission) return BadRequest(403);

                var user = _templateService.GetById(templateId);

                return Ok(user);
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
        /// <param name="templateForCreateDto"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Template created.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while adding template.</response>
        [HttpPost]
        public async Task<IActionResult> AddTemplateToProject(TemplateForCreateDto templateForCreateDto)
        {
            try
            {
                var permission = await _templateService.GetAccess(_currentUser.RoleId, ActionEnum.Create);
                if (!permission) return StatusCode(403);

                if (templateForCreateDto == null) return NotFound();

                var template = await _templateService.Create(templateForCreateDto);

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
        /// <param name="templateDtoForEdit"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Template updated.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">Template not found.</response>
        /// <response code="500">Something went wrong when updating the template.</response>
        [HttpPut]
        public async Task<IActionResult> EditExistingTemplateOfProject(TemplateForUpdateDto templateDtoForEdit)
        {
            try
            {
                var permission = await _templateService.GetAccess(_currentUser.RoleId, ActionEnum.Update);
                if (!permission) return StatusCode(403);

                var checker = await _templateService.Update(templateDtoForEdit);

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
        /// Delete existing template.
        /// </summary>
        /// <param name="templateId">Id of the template to be deleted.</param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Template was deleted successfully.</response>
        /// <response code="400">Invalid id.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">Template was not found.</response>
        /// <response code="500">Something went wrong while deleting template.</response>
        [HttpDelete]
        public async Task<IActionResult> Delete(int templateId)
        {
            try
            {
                var permission = await _templateService.GetAccess(_currentUser.RoleId, ActionEnum.Delete);
                if (!permission) return BadRequest(403);

                var checker = await _templateService.Delete(templateId);

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

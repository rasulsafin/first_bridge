using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using DM.Domain.Interfaces;
using DM.Domain.DTO;
using DM.Domain.Services;
using DM.Domain.Infrastructure.Exceptions;

using DM.Common.Enums;

using DM.Validators.Attributes;

using static DM.Validators.ServiceResponsesValidator;

namespace DM.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/organization")]
    public class OrganizationController : ControllerBase
    {
        private readonly UserDto _currentUser;

        private readonly IOrganizationService _organizationService;

        public OrganizationController(CurrentUserService currentUserService, IOrganizationService organizationService)
        {
            _currentUser = currentUserService.CurrentUser;
            _organizationService = organizationService;
        }

        /// <summary>
        /// Get list of all existing organizations.
        /// </summary>
        /// <returns>List of organizations.</returns>
        /// <response code="200">Organizations list fetched.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while fetching the organizations.</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var permission = await _organizationService.GetAccess(_currentUser.RoleId, ActionEnum.Read);

                if (!permission) return StatusCode(403);

                var organizations = await _organizationService.GetAll();

                return Ok(organizations);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        /// <summary>
        /// Create new organization.
        /// </summary>
        /// <param name="organizationForCreateDto"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Organization created.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while creating new organization.</response>
        [HttpPost]
        public async Task<IActionResult> Create(OrganizationForCreateDto organizationForCreateDto)
        {
            try
            {
                var permission = await _organizationService.GetAccess(_currentUser.RoleId, ActionEnum.Create);

                if (!permission) return StatusCode(403);

                var checker = await _organizationService.Create(organizationForCreateDto);

                return Ok(checker);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        /// <summary>
        /// Updating an existing organization.
        /// </summary>
        /// <param name="organizationForUpdateDto"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Organization updated.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while updating organization.</response>
        [HttpPut]
        public async Task<IActionResult> Update(OrganizationForUpdateDto organizationForUpdateDto)
        {
            try
            {
                var permission = await _organizationService.GetAccess(_currentUser.RoleId, ActionEnum.Update);

                if (!permission) return StatusCode(403);

                var checker = await _organizationService.Update(organizationForUpdateDto);

                if (!checker) return NotFound();

                return Ok(checker);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        /// <summary>
        /// Delete existing organization.
        /// </summary>
        /// <param name="organizationId">Id of the organization to be deleted.</param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Organization was deleted successfully.</response>
        /// <response code="400">Invalid id.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">Organization was not found.</response>
        /// <response code="500">Something went wrong while deleting organization.</response>
        [HttpDelete]
        public async Task<IActionResult> Delete(long organizationId)
        {
            try
            {
                var permission = await _organizationService.GetAccess(_currentUser.RoleId, ActionEnum.Delete);

                if (!permission) return StatusCode(403);

                var checker = await _organizationService.Delete(organizationId);

                if (!checker) return NotFound();

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

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Domain.Services;
using DM.Domain.Helpers;

using DM.DAL;
using DM.DAL.Enums;

using DM.Helpers;

using static DM.Validators.ServiceResponsesValidator;
using DM.Domain.Infrastructure.Exceptions;

namespace DM.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/organization")]
    public class OrganizationController : ControllerBase
    {
        private readonly DmDbContext _context;
        private readonly UserDto _currentUser;

        private readonly IOrganizationService _organizationService;
        private readonly ILogger<OrganizationService> _logger;

        public OrganizationController(DmDbContext context, CurrentUserService currentUserService,
            IOrganizationService organizationService, ILogger<OrganizationService> logger)
        {
            _context = context;
            _currentUser = currentUserService.CurrentUser;
            _organizationService = organizationService;
            _logger = logger;
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
                var permission = AuthorizationHelper.CheckUserPermissionsForRead(_context, _currentUser, PermissionEnum.Organization);

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
        /// <param name="organizationForCreateModel"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Organization created.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while creating new organization.</response>
        [HttpPost]
        public async Task<IActionResult> Create(OrganizationForCreateDto organizationForCreateModel)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForCreate(_context, _currentUser, PermissionEnum.Organization);

                if (!permission) return StatusCode(403);

                var checker = await _organizationService.Create(organizationForCreateModel);

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
        /// <param name="organizationForUpdateModel"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Organization updated.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while updating organization.</response>
        [HttpPut]
        public async Task<IActionResult> Update(OrganizationForUpdateDto organizationForUpdateModel)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForUpdate(_context, _currentUser, PermissionEnum.Organization);

                if (!permission) return StatusCode(403);

                var checker = await _organizationService.Update(organizationForUpdateModel);

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
                var permission = AuthorizationHelper.CheckUserPermissionsForDelete(_context, _currentUser, PermissionEnum.Organization);

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

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Domain.Implementations;
using DM.Domain.Helpers;

using DM.DAL;
using DM.DAL.Entities;

using DM.Helpers;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/organization")]
    public class OrganizationController : ControllerBase
    {
        private readonly DmDbContext _context;
        private readonly UserEntity _currentUser;

        private readonly IOrganizationService _organizationService;
        private readonly ILogger<OrganizationService> _logger;

        public OrganizationController(DmDbContext context, CurrentUserService currentUserService, IOrganizationService organizationService, ILogger<OrganizationService> logger)
        {
            _context = context;
            _currentUser = currentUserService.CurrentUser;
            _organizationService = organizationService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {

            var permission = AuthorizationHelper.CheckUserPermissionsForRead(_context, _currentUser, PermissionType.Organization);

            if (!permission) return StatusCode(403);

            var organizations = await _organizationService.GetAll();

            return Ok(organizations);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(OrganizationForCreateModel organizationModel)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForCreate(_context, _currentUser, PermissionType.Organization);

            if (!permission) return StatusCode(403);

            var checker = await _organizationService.Create(organizationModel);

            return Ok(checker);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(OrganizationForUpdateModel organizationModel)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForUpdate(_context, _currentUser, PermissionType.Organization);

            if (!permission) return StatusCode(403);

            var checker = await _organizationService.Update(organizationModel);

            if (checker == false) return BadRequest("No such organization exists");

            return Ok(checker);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(long organizationId)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForDelete(_context, _currentUser, PermissionType.Organization);

            if (!permission) return StatusCode(403);

            var checker = await _organizationService.Delete(organizationId);

            if (checker == false)
            {
                return NotFound();
            }

            return Ok(checker);
        }
    }
}

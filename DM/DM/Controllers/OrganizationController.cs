using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/organization")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [Authorize(RoleConst.AdminSuperAdmin)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var organizations = await _organizationService.GetAll();

            return Ok(organizations);
        }

        //[Authorize(RoleConst.OnlySuperAdmin)]
        [HttpPost]
        public async Task<IActionResult> Create(OrganizationModelForCreate organizationModel)
        {
            var checker = await _organizationService.Create(organizationModel);

            return Ok(checker);
        }

        [HttpPut]
        public async Task<IActionResult> Update(OrganizationModelForUpdate organizationModel)
        {
            var checker = await _organizationService.Update(organizationModel);

            if (checker == false) return BadRequest("No such organization exists");

            return Ok(checker);
        }
        
        [Authorize(RoleConst.UserAdmin)]
        [HttpDelete]
        public async Task<IActionResult> Delete(long organizationId)
        {
            var checker = await _organizationService.Delete(organizationId);

            if (checker == false)
            {
                return NotFound();
            }

            return Ok(checker);
        }
    }
}

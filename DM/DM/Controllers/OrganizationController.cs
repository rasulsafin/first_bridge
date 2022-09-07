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

        [Authorize(RoleConst.OnlySuperAdmin)]
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
            var id = await _organizationService.Create(organizationModel);

            return Ok(id);
        }
    }
}

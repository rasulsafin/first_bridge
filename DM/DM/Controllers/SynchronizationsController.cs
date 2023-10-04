using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using offline_module.Domain.Interfaces;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace offline_module.Controllers
{
    [ApiController]
    [Route("api/synchronization")]
    public class SynchronizationsController : ControllerBase
    {
        private ISynchronizationService _syncService;

        public SynchronizationsController(ISynchronizationService syncService) {
            _syncService = syncService;
        } 

        [HttpGet]
        public async Task<IActionResult> Synchronize()
        {
            try
            {
                await _syncService.Synchronize();
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during synchronization");
            }
            
        }
    }
}

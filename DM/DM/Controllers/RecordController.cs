using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/record")]
    public class RecordController : ControllerBase
    {
        public readonly IRecordService _recordService;

        public RecordController(IRecordService recordService)
        {
            _recordService = recordService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _recordService.GetAll();

            return Ok(users);
        }

        [Authorize]
        [HttpGet("{recordId}")]
        public IActionResult GetById(long recordId)
        {
            var record = _recordService.GetById(recordId);

            return Ok(record);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(RecordModel recordModel)
        {
            var id = await _recordService.Create(recordModel);

            return Ok(id);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(long recordId)
        {
            var id = await _recordService.Delete(recordId);

            return Ok(id);
        }
    }
}

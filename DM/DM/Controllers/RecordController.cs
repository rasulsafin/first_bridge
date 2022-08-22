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

        [Authorize(RoleConst.UserAdmin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _recordService.GetAll();

            return Ok(users);
        }

        [Authorize(RoleConst.UserAdmin)]
        [HttpGet("{recordId}")]
        public IActionResult GetById(long recordId)
        {
            var record = _recordService.GetById(recordId);

            return Ok(record);
        }

        [Authorize(RoleConst.UserAdmin)]
        [HttpPost]
        public async Task<IActionResult> Create(RecordModel recordModel)
        {
            var id = await _recordService.Create(recordModel);

            return Ok(id);
        }
        /*
        [Authorize(RoleConst.UserAdmin)]
        [HttpPut]
        public async Task<IActionResult> Update(FieldsModel fields)
        {
            var checker = await _recordService.Update(fields);

            return Ok(checker);
        }
        */
        [Authorize(RoleConst.UserAdmin)]
        [HttpDelete]
        public async Task<IActionResult> Delete(long recordId)
        {
            var id = await _recordService.Delete(recordId);

            return Ok(id);
        }
    }
}

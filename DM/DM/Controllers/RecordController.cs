using System.Linq;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DM.DAL.Entities;
using DM.repository;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/record")]
    public class RecordController : ControllerBase
    {
        public readonly IRecordService _recordService;
        public readonly DmDbContext _context;

        public RecordController(IRecordService recordService, DmDbContext context)
        {
            _recordService = recordService;
            _context = context;
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
            var userId = HttpContext.GetUserId();

            var permission = _context.Permissions.FirstOrDefault(x =>
                x.Type == PermissionType.Record && x.UserId == userId && x.ObjectId == recordId);
            if (permission == null || !permission.Read)
            {
                return BadRequest("you have no permissions to watch this record");
            }

            var record = _recordService.GetById(recordId);
            if (record == null)
                return NotFound();

            return Ok(record);
        }

        [Authorize(RoleConst.UserAdmin)]
        [HttpPost]
        public async Task<IActionResult> Create(RecordModel recordModel)
        {
            var id = await _recordService.Create(recordModel);

            return Ok(id);
        }

        [Authorize(RoleConst.UserAdmin)]
        [HttpPut]
        public async Task<IActionResult> Update(RecordModel recordModel)
        {
            var checker = await _recordService.Update(recordModel);

            if (checker == false)
            {
                return BadRequest("the fields must not contain invalid characters");
            }

            return Ok(checker);
        }


        [Authorize(RoleConst.UserAdmin)]
        [HttpDelete]
        public async Task<IActionResult> Delete(long recordId)
        {
            var checker = await _recordService.Delete(recordId);

            if (checker == false)
            {
                return NotFound();
            }

            return Ok(checker);
        }
    }
}

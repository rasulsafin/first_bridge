using System.Linq;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DM.DAL.Entities;
using DM.repository;
using DM.Domain.Helpers;
using DM.Domain.Implementations;
using DM.Entities;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/record")]
    public class RecordController : ControllerBase
    {
        public readonly IRecordService _recordService;
        public readonly DmDbContext _context;
        private readonly UserEntity _currentUser;

        public RecordController(IRecordService recordService, DmDbContext context, CurrentUserService userService)
        {
            _recordService = recordService;
            _context = context;
            _currentUser = userService.CurrentUser;
        }

        [Authorize(RoleConst.SuperAdmin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            // логика проверки доступа для GetAll перенесена в сервис
            var records = _recordService.GetAll();

            if (records.Count == 0)
            {
                return Ok("No records available at the moment");
            }

            return Ok(records);
        }

        [Authorize(RoleConst.UserAdmin)]
        [HttpGet("{recordId}")]
        public IActionResult GetById(long recordId)
        {
            var userId = HttpContext.GetUserId();

            var permission = AuthorizationHelper.CheckUsersPermissionsById(_context, _currentUser, PermissionType.Record, recordId);

            if (permission == null || !permission.Read)
            {
                return StatusCode(403);
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
            var permission = AuthorizationHelper.CheckUsersPermissionsForCreate(_context, _currentUser, PermissionType.Record);

            if (permission == null)
            {
                return StatusCode(403);
            }

            var id = await _recordService.Create(recordModel);

            if (id == 0)
            {
                return BadRequest();
            }

            return Ok(id);
        }

        [Authorize(RoleConst.UserAdmin)]
        [HttpPut]
        public async Task<IActionResult> Update(RecordModel recordModel)
        {
            var permission = AuthorizationHelper.CheckUsersPermissionsForUpdate(_context, _currentUser, PermissionType.Record, recordModel.Id);

            if (permission == null)
            {
                return StatusCode(403);
            }

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
            var permission = AuthorizationHelper.CheckUsersPermissionsForDelete(_context, _currentUser, PermissionType.Record, recordId);

            if (permission == null)
            {
                return StatusCode(403);
            }

            var checker = await _recordService.Delete(recordId);

            if (checker == false)
            {
                return NotFound();
            }

            return Ok(checker);
        }
    }
}

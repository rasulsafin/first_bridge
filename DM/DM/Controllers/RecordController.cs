using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DM.Domain.Interfaces;
using DM.Domain.Implementations;
using DM.Domain.Helpers;
using DM.Domain.Models;

using DM.DAL.Entities;
using DM.DAL;

using DM.Helpers;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/record")]
    public class RecordController : ControllerBase
    {
        private readonly DmDbContext _context;
        private readonly UserEntity _currentUser;

        private readonly IRecordService _recordService;
        private readonly ILogger<RecordService> _logger;

        public RecordController(DmDbContext context, CurrentUserService currentUserService, IRecordService recordService, ILogger<RecordService> logger)
        {
            _context = context;
            _currentUser = currentUserService.CurrentUser;
            _recordService = recordService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForRead(_context, _currentUser, PermissionType.Record);

            if (!permission) return StatusCode(403);

            var records = _recordService.GetAll();

            return Ok(records);
        }

        [HttpGet("{recordId}")]
        [Authorize]
        public IActionResult GetById(long recordId)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForRead(_context, _currentUser, PermissionType.Record);

            if (!permission) return StatusCode(403);

            var record = _recordService.GetById(recordId);

            if (record == null)
                return NotFound();

            return Ok(record);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(RecordModel recordModel)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForCreate(_context, _currentUser, PermissionType.Record);

            if (!permission) return StatusCode(403);

            var id = await _recordService.Create(recordModel);

            if (id == 0)
            {
                return BadRequest();
            }

            return Ok(id);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(RecordModel recordModel)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForUpdate(_context, _currentUser, PermissionType.Record);

            if (!permission) return StatusCode(403);

            var checker = await _recordService.Update(recordModel);

            if (!checker)
            {
                return BadRequest("the fields must not contain invalid characters");
            }

            return Ok(checker);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(long recordId)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForDelete(_context, _currentUser, PermissionType.Record);

            if (!permission) return StatusCode(403);

            var checker = await _recordService.Delete(recordId);

            if (!checker)
            {
                return NotFound();
            }

            return Ok(checker);
        }
    }
}

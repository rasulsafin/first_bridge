using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DM.Domain.Interfaces;
using DM.Domain.Implementations;
using DM.Domain.Helpers;
using DM.Domain.Models;
using DM.Domain.Exceptions;

using DM.DAL.Enums;
using DM.DAL;

using DM.Helpers;

using static DM.Validators.ServiceResponsesValidator;

namespace DM.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/record")]
    public class RecordController : ControllerBase
    {
        private readonly DmDbContext _context;
        private readonly UserModel _currentUser;

        private readonly IRecordService _recordService;
        private readonly ILogger<RecordService> _logger;

        public RecordController(DmDbContext context, CurrentUserService currentUserService, IRecordService recordService, ILogger<RecordService> logger)
        {
            _context = context;
            _currentUser = currentUserService.CurrentUser;
            _recordService = recordService;
            _logger = logger;
        }

        /// <summary>
        /// Get list of all existing records.
        /// </summary>
        /// <returns>List of records.</returns>
        /// <response code="200">Records list fetched.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while fetching the records.</response>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForRead(_context, _currentUser, PermissionEnum.Record);

                if (!permission) return StatusCode(403);

                var records = _recordService.GetAll();

                return Ok(records);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        /// <summary>
        /// Get record by their id.
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns>Record Id.</returns>
        /// <response code="200">Record found.</response>
        /// <response code="400">Invalid id.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">Could not find record.</response>
        /// <response code="500">Something went wrong while fetching the record.</response>
        [HttpGet("{recordId}")]
        public IActionResult GetById(long recordId)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForRead(_context, _currentUser, PermissionEnum.Record);

                if (!permission) return StatusCode(403);

                var record = _recordService.GetById(recordId);

                if (record == null) return NotFound();

                return Ok(record);
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

        /// <summary>
        /// Create new record.
        /// </summary>
        /// <param name="recordModel"></param>
        /// <returns>Id of created record.</returns>        
        /// <response code="200">Record created.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while creating new record.</response>
        [HttpPost]
        public async Task<IActionResult> Create(RecordForCreateModel recordModel)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForCreate(_context, _currentUser, PermissionEnum.Record);

                if (!permission) return StatusCode(403);

                var id = await _recordService.Create(recordModel);

                if (id == 0) return BadRequest();

                return Ok(id);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        /// <summary>
        /// Updating an existing record.
        /// </summary>
        /// <param name="recordModel"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Record updated.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while updating record.</response>
        [HttpPut]
        public async Task<IActionResult> Update(RecordModel recordModel)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForUpdate(_context, _currentUser, PermissionEnum.Record);

                if (!permission) return StatusCode(403);

                var checker = await _recordService.Update(recordModel);

                if (!checker) return BadRequest();

                return Ok(checker);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        /// <summary>
        /// Delete existing record.
        /// </summary>
        /// <param name="recordId">Id of the record to be deleted.</param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Record was deleted successfully.</response>
        /// <response code="400">Invalid id.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">Record was not found.</response>
        /// <response code="500">Something went wrong while deleting record.</response>
        [HttpDelete]
        public async Task<IActionResult> Delete(long recordId)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForDelete(_context, _currentUser, PermissionEnum.Record);

                if (!permission) return StatusCode(403);

                var checker = await _recordService.Delete(recordId);

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

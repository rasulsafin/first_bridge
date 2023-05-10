using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using DM.Domain.Interfaces;
using DM.Domain.DTO;
using DM.Domain.Services;
using DM.Domain.Infrastructure.Exceptions;

using DM.Validators.Attributes;

using static DM.Validators.ServiceResponsesValidator;
using DM.Common.Enums;

namespace DM.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/record")]
    public class RecordController : ControllerBase
    {
        private readonly UserDto _currentUser;

        private readonly IRecordService _recordService;

        public RecordController(CurrentUserService currentUserService, IRecordService recordService)
        {
            _currentUser = currentUserService.CurrentUser;
            _recordService = recordService;
        }

        /// <summary>
        /// Get list of all existing records.
        /// </summary>
        /// <returns>List of records.</returns>
        /// <response code="200">Records list fetched.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while fetching the records.</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var permission = await _recordService.GetAccess(_currentUser.RoleId, ActionEnum.Read);

                if (!permission) return StatusCode(403);

                var records = await _recordService.GetAll();

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
        public async Task<IActionResult> GetById(long recordId)
        {
            try
            {
                var permission = await _recordService.GetAccess(_currentUser.RoleId, ActionEnum.Read);

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
        /// <param name="recordDto"></param>
        /// <returns>Id of created record.</returns>        
        /// <response code="200">Record created.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while creating new record.</response>
        [HttpPost]
        public async Task<IActionResult> Create(RecordForCreateDto recordDto)
        {
            try
            {
                var permission = await _recordService.GetAccess(_currentUser.RoleId, ActionEnum.Create);

                if (!permission) return StatusCode(403);

                var id = await _recordService.Create(recordDto);

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
        /// <param name="recordDto"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Record updated.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while updating record.</response>
        [HttpPut]
        public async Task<IActionResult> Update(RecordDto recordDto)
        {
            try
            {
                var permission = await _recordService.GetAccess(_currentUser.RoleId, ActionEnum.Update);

                if (!permission) return StatusCode(403);

                var checker = await _recordService.Update(recordDto);

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
                var permission = await _recordService.GetAccess(_currentUser.RoleId, ActionEnum.Delete);

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

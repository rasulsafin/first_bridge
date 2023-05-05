using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using DM.Domain.Interfaces;
using DM.Domain.DTO;
using DM.Domain.Services;
using DM.Domain.Infrastructure.Exceptions;

using DM.Common.Enums;

using DM.Validators.Attributes;

using static DM.Validators.ServiceResponsesValidator;

namespace DM.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/field")]
    public class FieldController : ControllerBase
    {
        private readonly UserDto _currentUser;

        private readonly IFieldService _fieldService;

        public FieldController(CurrentUserService currentUserService, IFieldService fieldService)
        {
            _currentUser = currentUserService.CurrentUser;
            _fieldService = fieldService;
        }

        /// <summary>
        /// Create new field.
        /// </summary>
        /// <param name="fieldDto"></param>
        /// <returns>Id of created field.</returns>        
        /// <response code="200">Field created.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while creating new field.</response>
        [HttpPost]
        public async Task<IActionResult> Create(FieldDto fieldDto)
        {
            try
            {
                var permission = await _fieldService.GetAccess(_currentUser.RoleId);

                if (!permission.Create) return StatusCode(403);

                var checker = await _fieldService.Create(fieldDto);

                return Ok(checker);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        /// <summary>
        /// Delete existing field.
        /// </summary>
        /// <param name="fieldId">Id of the field to be deleted.</param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Field was deleted successfully.</response>
        /// <response code="400">Invalid id.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">Field was not found.</response>
        /// <response code="500">Something went wrong while deleting field.</response>
        [HttpDelete]
        public async Task<IActionResult> Delete(long fieldId)
        {
            try
            {
                var permission = await _fieldService.GetAccess(_currentUser.RoleId);

                if (!permission.Delete) return StatusCode(403);

                var checker = await _fieldService.Delete(fieldId);

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

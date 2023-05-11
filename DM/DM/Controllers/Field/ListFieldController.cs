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
    [Route("api/listField")]
    public class ListFieldController : ControllerBase
    {
        private readonly UserDto _currentUser;
        private readonly IListFieldService _listFieldService;

        public ListFieldController(CurrentUserService currentUserService, IListFieldService listFieldService)
        {
            _currentUser = currentUserService.CurrentUser;
            _listFieldService = listFieldService;
        }

        /// <summary>
        /// Create new listField.
        /// </summary>
        /// <param name="listFieldDto"></param>
        /// <returns>Id of created listField.</returns>        
        /// <response code="200">ListField created.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while creating new listField.</response>
        [HttpPost]
        public async Task<IActionResult> Create(ListFieldDto listFieldDto)
        {
            try
            {
                var permission = await _listFieldService.GetAccess(_currentUser.RoleId, ActionEnum.Create);

                if (!permission) return StatusCode(403);

                var checker = await _listFieldService.Create(listFieldDto);

                return Ok(checker);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        /// <summary>
        /// Delete existing listField.
        /// </summary>
        /// <param name="listFieldId">Id of the listField to be deleted.</param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">ListField was deleted successfully.</response>
        /// <response code="400">Invalid id.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">ListField was not found.</response>
        /// <response code="500">Something went wrong while deleting listField.</response>
        [HttpDelete]
        public async Task<IActionResult> Delete(long listFieldId)
        {
            try
            {
                var permission = await _listFieldService.GetAccess(_currentUser.RoleId, ActionEnum.Delete);

                if (!permission) return StatusCode(403);

                var checker = await _listFieldService.Delete(listFieldId);

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

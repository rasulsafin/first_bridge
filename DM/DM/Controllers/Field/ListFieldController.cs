using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Domain.Helpers;
using DM.Domain.Services;

using DM.DAL.Enums;
using DM.DAL;

using DM.Helpers;

using static DM.Validators.ServiceResponsesValidator;
using DM.Domain.Infrastructure.Exceptions;

namespace DM.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/listField")]
    public class ListFieldController : ControllerBase
    {
        private readonly DmDbContext _context;
        private readonly UserDto _currentUser;

        private readonly IListFieldService _listFieldService;
        private readonly ILogger<FieldService> _logger;

        public ListFieldController(DmDbContext context, CurrentUserService currentUserService, IListFieldService listFieldService, ILogger<FieldService> logger)
        {
            _context = context;
            _currentUser = currentUserService.CurrentUser;
            _listFieldService = listFieldService;
            _logger = logger;
        }

        /// <summary>
        /// Create new listField.
        /// </summary>
        /// <param name="listFieldModel"></param>
        /// <returns>Id of created listField.</returns>        
        /// <response code="200">ListField created.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while creating new listField.</response>
        [HttpPost]
        public async Task<IActionResult> Create(ListFieldDto listFieldModel)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForCreate(_context, _currentUser, PermissionEnum.Template);

                if (!permission) return StatusCode(403);

                var checker = await _listFieldService.Create(listFieldModel);

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
                var permission = AuthorizationHelper.CheckUserPermissionsForDelete(_context, _currentUser, PermissionEnum.Template);

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

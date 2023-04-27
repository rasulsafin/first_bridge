using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Domain.Helpers;
using DM.Domain.Implementations;
using DM.Domain.Exceptions;

using DM.DAL.Enums;
using DM.DAL;

using DM.Helpers;

using static DM.Validators.ServiceResponsesValidator;
using System.Threading.Tasks;

namespace DM.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/field")]
    public class FieldController : ControllerBase
    {
        private readonly DmDbContext _context;
        private readonly UserModel _currentUser;

        private readonly IFieldService _fieldService;
        private readonly ILogger<FieldService> _logger;

        public FieldController(DmDbContext context, CurrentUserService currentUserService, IFieldService fieldService, ILogger<FieldService> logger)
        {
            _context = context;
            _currentUser = currentUserService.CurrentUser;
            _fieldService = fieldService;
            _logger = logger;
        }

        /// <summary>
        /// Create new field.
        /// </summary>
        /// <param name="fieldModel"></param>
        /// <returns>Id of created field.</returns>        
        /// <response code="200">Field created.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while creating new field.</response>
        [HttpPost]
        public async Task<IActionResult> Create(FieldModel fieldModel)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForCreate(_context, _currentUser, PermissionEnum.Template);

                if (!permission) return StatusCode(403);

                var checker = await _fieldService.Create(fieldModel);

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
                var permission = AuthorizationHelper.CheckUserPermissionsForDelete(_context, _currentUser, PermissionEnum.Template);

                if (!permission) return StatusCode(403);

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

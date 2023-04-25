using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Domain.Helpers;
using DM.Domain.Implementations;

using DM.DAL.Entities;
using DM.DAL.Enums;
using DM.DAL;

using DM.Helpers;

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

        [HttpPost]
        public IActionResult Create(FieldModel fieldModel)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForCreate(_context, _currentUser, PermissionType.Template);

            if (!permission) return StatusCode(403);

            var checker = _fieldService.Create(fieldModel);

            return Ok(checker);
        }

        [HttpDelete]
        public IActionResult Delete(long fieldId)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForDelete(_context, _currentUser, PermissionType.Template);

            if (!permission) return StatusCode(403);

            var checker = _fieldService.Delete(fieldId);

            if (checker == false)
            {
                return NotFound();
            }

            return Ok(checker);
        }
    }

    [ApiController]
    [Authorize]
    [Route("api/listField")]
    public class ListFieldController : ControllerBase
    {
        private readonly DmDbContext _context;
        private readonly UserModel _currentUser;

        private readonly IListFieldService _listFieldService;
        private readonly ILogger<FieldService> _logger;

        public ListFieldController(DmDbContext context, CurrentUserService currentUserService, IListFieldService listFieldService, ILogger<FieldService> logger)
        {
            _context = context;
            _currentUser = currentUserService.CurrentUser;
            _listFieldService = listFieldService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Create(ListFieldModel listFieldModel)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForCreate(_context, _currentUser, PermissionType.Template);

            if (!permission) return StatusCode(403);

            var checker = _listFieldService.Create(listFieldModel);

            return Ok(checker);
        }

        [HttpDelete]
        public IActionResult Delete(long listFieldId)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForDelete(_context, _currentUser, PermissionType.Template);

            if (!permission) return StatusCode(403);

            var checker = _listFieldService.Delete(listFieldId);

            if (checker == false)
            {
                return NotFound();
            }

            return Ok(checker);
        }
    }
}

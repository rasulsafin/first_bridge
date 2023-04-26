using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Domain.Helpers;
using DM.Domain.Implementations;

using DM.DAL.Enums;
using DM.DAL;

using DM.Helpers;

namespace DM.Controllers
{
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

            if (!checker) return NotFound();

            return Ok(checker);
        }
    }
}

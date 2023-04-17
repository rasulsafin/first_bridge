using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DM.Domain.Exceptions;
using DM.Domain.Helpers;
using DM.Domain.Implementations;
using DM.Domain.Interfaces;
using DM.Domain.Models;

using DM.DAL;
using DM.DAL.Entities;

using DM.Helpers;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        public readonly DmDbContext _context;
        private readonly UserEntity _currentUser;

        public readonly ICommentService _commentService;
        private readonly ILogger<FieldService> _logger;

        public CommentController(DmDbContext context, CurrentUserService currentUserService, ICommentService commentService, ILogger<FieldService> logger)
        {
            _context = context;
            _currentUser = currentUserService.CurrentUser;
            _commentService = commentService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CommentModel commentModel)
        {

            var permission = AuthorizationHelper.CheckUserPermissionsForCreate(_context, _currentUser, PermissionType.Record);

            if (!permission) return StatusCode(403);

            var id = await _commentService.Create(commentModel);

            if (id == 0)
            {
                return BadRequest();
            }

            return Ok(id);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(long commentId)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForDelete(_context, _currentUser, PermissionType.Record);

            if (!permission) return StatusCode(403);

            var checker = await _commentService.Delete(commentId);

            if (!checker)
            {
                return NotFound();
            }

            return Ok(checker);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(CommentModelForUpdate comment)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForUpdate(_context, _currentUser, PermissionType.Record);

                if (!permission) return StatusCode(403);

                var checker = await _commentService.Update(comment);
                return Ok(checker);
            }
            catch (ArgumentValidationException ex)
            {
                return BadRequest("something went wrong");
            }
        }
    }
}
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using DM.Domain.Exceptions;
using DM.Domain.Helpers;
using DM.Domain.Implementations;
using DM.Domain.Interfaces;
using DM.Domain.Models;

using DM.DAL;
using DM.DAL.Entities;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        public readonly DmDbContext _context;
        private readonly UserEntity _currentUser;

        public readonly ICommentService _commentService;

        public CommentController(DmDbContext context, CurrentUserService userService, ICommentService commentService)
        {
            _context = context;
            _currentUser = userService.CurrentUser;
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommentModel commentModel)
        {

            var permission = AuthorizationHelper.CheckUserPermissionsForUpdate(_context, _currentUser, PermissionType.Record);

            if (!permission)
            {
                return StatusCode(403);
            }

            var id = await _commentService.Create(commentModel);

            if (id == 0)
            {
                return BadRequest();
            }

            return Ok(id);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long commentId)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForDelete(_context, _currentUser, PermissionType.Record);

            if (!permission)
            {
                return StatusCode(403);
            }

            var checker = await _commentService.Delete(commentId);

            if (!checker)
            {
                return NotFound();
            }

            return Ok(checker);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CommentModelForUpdate comment)
        {
            try
            {
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
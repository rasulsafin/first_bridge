using System.Linq;
using System.Threading.Tasks;
using DM.DAL;
using DM.DAL.Entities;
using DM.Domain.Exceptions;
using DM.Domain.Helpers;
using DM.Domain.Implementations;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        public readonly ICommentService _commentService;
        public readonly DmDbContext _context;
        private readonly UserEntity _currentUser;

        public CommentController(ICommentService commentService, DmDbContext context, CurrentUserService userService)
        {
            _commentService = commentService;
             _context = context;
             _currentUser = userService.CurrentUser;
        }
        
        // метод getAllCommentsOfRecord вшит в RecordService
        
        [Authorize(RoleConst.UserAdmin)]
        [HttpPost]
        public async Task<IActionResult> Create(CommentModel commentModel)
        {
            
            var permission = AuthorizationHelper.CheckUsersPermissionsForUpdate(_context, _currentUser, PermissionType.Record, commentModel.RecordId);

            if (permission == null)
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
            // проверяем доступ к удалению записи
            var record = await _context.Records.Where(q => q.Comments.Any(p => p.Id == commentId)).FirstOrDefaultAsync();
            var permission = AuthorizationHelper.CheckUsersPermissionsForDelete(_context, _currentUser, PermissionType.Record, record.Id);

            if (permission == null)
            {
                return StatusCode(403);
            }

            var checker = await _commentService.Delete(commentId);

            if (checker == false)
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
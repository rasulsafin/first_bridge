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
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private readonly UserDto _currentUser;
        public readonly ICommentService _commentService;

        public CommentController(CurrentUserService currentUserService, ICommentService commentService)
        {
            _currentUser = currentUserService.CurrentUser;
            _commentService = commentService;
        }

        /// <summary>
        /// Create new comment.
        /// </summary>
        /// <param name="commentDto"></param>
        /// <returns>Id of created comment.</returns>        
        /// <response code="200">Comment created.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while creating new comment.</response>
        [HttpPost]
        public async Task<IActionResult> Create(CommentDto commentDto)
        {
            try
            {
                var permission = await _commentService.GetAccess(_currentUser.RoleId, ActionEnum.Create);
                if (!permission) return StatusCode(403);

                var id = await _commentService.Create(commentDto);

                if (id == 0) return BadRequest();

                return Ok(id);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        /// <summary>
        /// Delete existing comment.
        /// </summary>
        /// <param name="commentId">Id of the comment to be deleted.</param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Comment was deleted successfully.</response>
        /// <response code="400">Invalid id.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">Comment was not found.</response>
        /// <response code="500">Something went wrong while deleting comment.</response>
        [HttpDelete]
        public async Task<IActionResult> Delete(long commentId)
        {
            try
            {
                var permission = await _commentService.GetAccess(_currentUser.RoleId, ActionEnum.Delete);
                if (!permission) return StatusCode(403);

                var checker = await _commentService.Delete(commentId);

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

        /// <summary>
        /// Updating an existing comment.
        /// </summary>
        /// <param name="commentForUpdateDto"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Comment updated.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while updating comment.</response>
        [HttpPut]
        public async Task<IActionResult> Update(CommentForUpdateDto commentForUpdateDto)
        {
            try
            {
                var permission = await _commentService.GetAccess(_currentUser.RoleId, ActionEnum.Update);
                if (!permission) return StatusCode(403);

                var checker = await _commentService.Update(commentForUpdateDto);

                return Ok(checker);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }
    }
}
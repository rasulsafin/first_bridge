using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DM.Domain.Exceptions;
using DM.Domain.Helpers;
using DM.Domain.Implementations;
using DM.Domain.Interfaces;
using DM.Domain.Models;

using DM.DAL;
using DM.DAL.Enums;

using DM.Helpers;

using static DM.Validators.ServiceResponsesValidator;

namespace DM.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        public readonly DmDbContext _context;
        private readonly UserModel _currentUser;

        public readonly ICommentService _commentService;
        private readonly ILogger<FieldService> _logger;

        public CommentController(DmDbContext context, CurrentUserService currentUserService, ICommentService commentService, ILogger<FieldService> logger)
        {
            _context = context;
            _currentUser = currentUserService.CurrentUser;
            _commentService = commentService;
            _logger = logger;
        }

        /// <summary>
        /// Create new comment.
        /// </summary>
        /// <param name="commentModel"></param>
        /// <returns>Id of created comment.</returns>        
        /// <response code="200">Comment created.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while creating new comment.</response>
        [HttpPost]
        public async Task<IActionResult> Create(CommentModel commentModel)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForCreate(_context, _currentUser, PermissionEnum.Record);

                if (!permission) return StatusCode(403);

                var id = await _commentService.Create(commentModel);

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
                var permission = AuthorizationHelper.CheckUserPermissionsForDelete(_context, _currentUser, PermissionEnum.Record);

                if (!permission) return StatusCode(403);

                var checker = await _commentService.Delete(commentId);

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

        /// <summary>
        /// Updating an existing comment.
        /// </summary>
        /// <param name="commentForUpdateModel"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Comment updated.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while updating comment.</response>
        [HttpPut]
        public async Task<IActionResult> Update(CommentForUpdateModel commentForUpdateModel)
        {
            try
            {
                var permission = AuthorizationHelper.CheckUserPermissionsForUpdate(_context, _currentUser, PermissionEnum.Record);

                if (!permission) return StatusCode(403);

                var checker = await _commentService.Update(commentForUpdateModel);

                return Ok(checker);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }
    }
}
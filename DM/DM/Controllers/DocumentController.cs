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
    [Route("api/document")]
    public class DocumentController : Controller
    {
        private readonly UserDto _currentUser;
        private readonly IDocumentService _documentService;

        public DocumentController(CurrentUserService currentUserService, IDocumentService documentService)
        {
            _currentUser = currentUserService.CurrentUser;
            _documentService = documentService;
        }

        /// <summary>
        /// Get list of all existing documents.
        /// </summary>
        /// <returns>List of documents.</returns>
        /// <response code="200">Documents list fetched.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while fetching the documents.</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var permission = await _documentService.GetAccess(_currentUser.RoleId, ActionEnum.Read);
                if (!permission) return StatusCode(403);

                var records = await _documentService.GetAll();
                return Ok(records);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        /// <summary>
        /// Get document by their id.
        /// </summary>
        /// <param name="documentid"></param>
        /// <returns>Document Id.</returns>
        /// <response code="200">Document found.</response>
        /// <response code="400">Invalid id.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">Could not find document.</response>
        /// <response code="500">Something went wrong while fetching the document.</response>
        [HttpGet("{documentid}")]
        public async Task<IActionResult> GetById(long documentId)
        {
            try
            {
                var permission = await _documentService.GetAccess(_currentUser.RoleId, ActionEnum.Read);
                if (!permission) return StatusCode(403);

                var record = _documentService.GetById(documentId);
                if (record == null) return NotFound();

                return Ok(record);
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
        /// Create new document.
        /// </summary>
        /// <param name="documentDto"></param>
        /// <returns>Id of created document.</returns>        
        /// <response code="200">Document created.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while creating new document.</response>
        [HttpPost]
        public async Task<IActionResult> Create(DocumentDto documentDto)
        {
            try
            {
                var permission = await _documentService.GetAccess(_currentUser.RoleId, ActionEnum.Create);
                if (!permission) return StatusCode(403);

                var id = await _documentService.Create(documentDto);
                if (id == 0) return BadRequest();

                return Ok(id);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        /// <summary>
        /// Updating an existing document.
        /// </summary>
        /// <param name="documentDto"></param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Document updated.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while updating document.</response>
        [HttpPut]
        public async Task<IActionResult> Update(DocumentDto documentDto)
        {
            try
            {
                var permission = await _documentService.GetAccess(_currentUser.RoleId, ActionEnum.Update);
                if (!permission) return StatusCode(403);

                var checker = await _documentService.Update(documentDto);
                if (!checker) return BadRequest();

                return Ok(checker);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        /// <summary>
        /// Delete existing document.
        /// </summary>
        /// <param name="documentId">Id of the document to be deleted.</param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">Document was deleted successfully.</response>
        /// <response code="400">Invalid id.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">Document was not found.</response>
        /// <response code="500">Something went wrong while deleting document.</response>
        [HttpDelete]
        public async Task<IActionResult> Delete(long documentId)
        {
            try
            {
                var permission = await _documentService.GetAccess(_currentUser.RoleId, ActionEnum.Delete);
                if (!permission) return StatusCode(403);

                var checker = await _documentService.Delete(documentId);
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

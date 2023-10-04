using System.IO;
using System.Threading.Tasks;
using SO = System.IO.File;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Xbim.Ifc;
using Xbim.ModelGeometry.Scene;

using DM.Domain.Services;
using DM.Domain.Interfaces;
using DM.Domain.DTO;
using DM.Domain.Infrastructure.Exceptions;

using DM.Common.Helpers;
using DM.Common.Enums;

using DM.Validators.Attributes;

using static DM.Validators.ServiceResponsesValidator;
using System.Collections.Generic;
using System.Threading;
using DM.DAL.Entities;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/item")]
    public class ItemController : ControllerBase
    {
        private readonly UserDto _currentUser;
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService, CurrentUserService userService)
        {
            _itemService = itemService;
            _currentUser = userService.CurrentUser;
        }

        /// <summary>
        /// Get records about all documents
        /// </summary>
        /// <returns>list of items</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(long projectId)
        {
            try
            {
                var permission = true;
                if (!permission) return StatusCode(403);

                var items = await _itemService.GetAll(projectId);

                return Ok(items);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpGet("getById/{itemId}")]
        public async Task<IActionResult> GetById(int itemId)
        {
            try
            {
                var item = _itemService.GetById(itemId);
                return Ok(item);
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// Download file with Name specified in Db
        /// </summary>
        [HttpGet("download")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var permission = await _itemService.GetAccess(_currentUser.RoleId, ActionEnum.Read);
            if (!permission) return StatusCode(403);

            var file = _itemService.Find(fileName);
            if (file == null) return BadRequest($"File with name={fileName} Not Found.");

            var filePath = file.RelativePath;
            var bytes = await SO.ReadAllBytesAsync(filePath);

            return File(bytes, FileHelper.GetFileTypes(Path.GetExtension(fileName)), fileName);
        }

        [HttpGet("downloadWexBim")]
        public async Task<IActionResult> DownloadWexBim(string fileName) // название файла вместе с расширением .ifc
        {
            var permission = await _itemService.GetAccess(_currentUser.RoleId, ActionEnum.Read);
            if (!permission) return StatusCode(403);

            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            var file = _itemService.Find(fileName);

            if (file == null) return BadRequest("Such file does not exist");

            if (fileName == null) return BadRequest("fileName is empty");

            // проверка формата файла
            if (Path.GetExtension(fileName) != ".ifc") return BadRequest("Incorrect file format");

            var storagePath = FileHelper.PathServerStorage + fileName;

            // проверка существования готового wexBim
            var pathIfExist = FileHelper.CurrentPathServerStorage + Path.GetFileNameWithoutExtension(fileName) + ".wexBim";
            if (SO.Exists(pathIfExist))
            {
                var resultIfExist = SO.OpenRead(pathIfExist);
                return File(resultIfExist, "application/octet-stream", "file.wexBim");
            }

            // конвертация из ifc в wexBim
            using var model = IfcStore.Open(storagePath);
            var context = new Xbim3DModelContext(model);
            context.CreateContext();

            var wexBimFilename = Path.ChangeExtension(fileName, "wexBim");

            var newStoragePath = FileHelper.CurrentPathServerStorage + wexBimFilename;

            await using var wexBimFile = SO.Create(wexBimFilename);

            await using var wexBimBinaryWriter = new BinaryWriter(wexBimFile);
            model.SaveAsWexBim(wexBimBinaryWriter);
            wexBimBinaryWriter.Close();

            var result = SO.OpenRead(newStoragePath);

            return File(result, "application/octet-stream", "file.wexBim");
        }

        /// <summary>
        /// Upload file with versioning
        /// </summary>
        /// <param name="project"></param>
        /// <param name="file"></param>
        /// <returns>id of uploaded file</returns>
        /// <response code="200">File added.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="500">Something went wrong while adding new file.</response>
        [HttpPost, DisableRequestSizeLimit, Route("file")]
        public async Task<IActionResult> Post(long project, IFormFile file)
        {
            try
            {
                var permission = await _itemService.GetAccess(_currentUser.RoleId, ActionEnum.Create);
                if (!permission) return StatusCode(403);

                var item = await _itemService.Create(new ItemDto { ProjectId = project }, file);
                return Ok(item);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        [HttpPost("upload_files/{userId}")] 
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadItems(
            [FromRoute]
            int userId,
            [FromBody]
            IEnumerable<int> itemIds)
        {
            try
            {
                var items = await _itemService.UploadItems(userId, itemIds);
                return Ok(items);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }

        [HttpPost("download_files/{userId}")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> DownloadItems(
            [FromRoute]
            int userId,
            [FromBody]
            IEnumerable<int> itemIds)
        {
            try
            {
                var items = await _itemService.DownloadItems(userId, itemIds);
                return Ok(items);
            }
            catch (DocumentManagementException ex)
            {
                return CreateProblemResult(this, 500, ex.Message);
            }
        }


        [HttpPut("link_item/{projId}")]
        public async Task<IActionResult> LinkItem(
            [FromRoute]
            int projId,
            [FromBody]
            ItemDto itemDto)
        {
            var result = await _itemService.LinkItem(projId, itemDto);
            return Ok(result);
        }

        /// <summarys
        /// Delete existing user.
        /// </summary>
        /// <param name="userId">Id of the user to be deleted.</param>
        /// <returns>Boolean value about function execution.</returns>        
        /// <response code="200">User was deleted successfully.</response>
        /// <response code="400">Invalid id.</response>
        /// <response code="403">Access denied.</response>
        /// <response code="404">User was not found.</response>
        /// <response code="500">Something went wrong while deleting user.</response>
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(string fileName)
        {
            try
            {
                var permission = await _itemService.GetAccess(_currentUser.RoleId, ActionEnum.Delete);
                if (!permission) return BadRequest(403);

                var checker = _itemService.Delete(fileName);

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

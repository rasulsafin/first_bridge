using DM.DAL.Entities;
using DM.Domain.Helpers;
using DM.Domain.Implementations;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DM.DAL;
using Xbim.Ifc;
using Xbim.ModelGeometry.Scene;
using SO = System.IO.File;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/item")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        public readonly DmDbContext _context;
        private readonly UserEntity _currentUser;
        public static string pathServerStorage = "C:\\others\\";
        private static string currentPathServerStorage = "E:\\full-project\\document-manager\\DM\\DM\\";
        private int lastVersion = 1;  // variable for version tracking
        private static string GetMimeTypes(string ext)
        {
            switch (ext)
            {
                case ".txt": return "text/plain";
                case ".csv": return "text/csv";
                case ".pdf": return "application/pdf";
                case ".doc": return "application/vnd.ms-word";
                case ".xls": return "application/vnd.ms-excel";
                case ".ppt": return "application/vnd.ms-powerpoint";
                case ".docx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".xlsx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ".pptx": return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                case ".png": return "image/png";
                case ".jpg": return "image/jpeg";
                case ".jpeg": return "image/jpeg";
                case ".gif": return "image/gif";
                default: return "application/octet-stream";
            }
        }

        public ItemController(IItemService itemService, DmDbContext context, CurrentUserService userService)
        {
            _itemService = itemService;
            _context = context;
            _currentUser = userService.CurrentUser;
        }

        /// <summary>
        /// Get records about all documents
        /// </summary>
        /// <returns>list of items</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(long projectId)
        {
            // логика проверки доступа для GetAll перенесена в сервис
            var items = await _itemService.GetAll(projectId);

            return Ok(items);
        }

        /// <summary>
        /// Download file with Name specified in Db
        /// </summary>
        [HttpGet("download")]
        [Authorize]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var file = await _context.Items.FirstOrDefaultAsync(x => x.Name == fileName);
            if (file == null)
            {
                return BadRequest($"File with name={fileName} Not Found.");
            }

            var folderName = fileName.Remove(fileName.Length - 7);
            // var filePath = pathServerStorage + folderName + "\\" + fileName;
            var filePath = file.RelativePath;

            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, GetMimeTypes(Path.GetExtension(fileName)), fileName);
        }

        [HttpGet("downloadWexBim")]
        [Authorize]
        public async Task<IActionResult> DownloadWexBim(string fileName) // название файла вместе с расширением .ifc
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            var fileId = await _context.Items.Where(x => x.Name.Contains(fileNameWithoutExtension)).Select(q => q.Id).FirstOrDefaultAsync();

            if (fileId == 0)
            {
                return BadRequest("Such file does not exist");
            }

            //TODO: вернуть пермишны перед деплоем

            /*
            var permission = AuthorizationHelper.CheckUsersPermissionsById(_context, _currentUser, PermissionType.Item, fileId);
            
            if (permission == null || !permission.Read)
            {
                return StatusCode(403);
            }
            */

            if (fileName == null)
            {
                return BadRequest("fileName is empty");
            }

            // проверка формата файла
            if (Path.GetExtension(fileName) != ".ifc")
            {
                return BadRequest("Incorrect file format");
            }

            var storagePath = pathServerStorage + fileName;

            // проверка существования готового wexBim
            var pathIfExist = currentPathServerStorage + Path.GetFileNameWithoutExtension(fileName) + ".wexBim";
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

            var newStoragePath = currentPathServerStorage + wexBimFilename;

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
        /// <returns>id of uploaded file</returns>
        [HttpPost, DisableRequestSizeLimit, Route("file")]
        [Authorize]
        public async Task<IActionResult> Post(long project, IFormFile file)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForCreate(_context, _currentUser, PermissionType.Item);

            if (!permission)
            {
                return StatusCode(403);
            }

            var fileExtension = Path.GetExtension(file.FileName);
            var fileNameWithoutExtension = file.FileName.Remove(file.FileName.Length - 4); // Folder Name
            var pathSaveFile = pathServerStorage + fileNameWithoutExtension;

            if (fileExtension == ".jpg" || fileExtension == ".png" || fileExtension == ".bim" || fileExtension == ".ifc")
            {
                if (!Directory.Exists(pathSaveFile)) // Check the directory exists
                {
                    Directory.CreateDirectory(pathSaveFile);
                }

                var files = Directory.GetFiles(pathSaveFile);

                if (files.Length > 0) // Check if folder contains files with certain name
                {
                    foreach (var a in Directory.GetFiles(pathSaveFile))
                    {

                        if (a.Contains(lastVersion.ToString()))
                        {
                            lastVersion += 1;
                        }
                    }
                }

                var pathForCreate = pathServerStorage + fileNameWithoutExtension + @"\" + fileNameWithoutExtension + "_v" + lastVersion + fileExtension;

                using (var fstream = new FileInfo(pathForCreate).Create()) // Create instance to put an Object
                {
                    var c = fstream;
                    await file.CopyToAsync(fstream); // Put an Object
                }

                var itemModel = new ItemModel()
                {
                    Name = fileNameWithoutExtension + "_v" + lastVersion + fileExtension,
                    RelativePath = pathForCreate,
                    ProjectId = project
                };
                var item = await _itemService.Create(itemModel); // Adding a Record about new Item
                return Ok(item);
            }
            else
            { return BadRequest(new { message = "invalid file format" }); }
        }
    }
}

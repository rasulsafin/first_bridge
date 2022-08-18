using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/item")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private static string pathServerStorage = @$"E:\others\";
        private int lastVersion = 1;  // variable for version tracking

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        /// <summary>
        /// Get records about all documents
        /// </summary>
        /// <returns>list of items</returns>
        [Authorize(RoleConst.UserAdmin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _itemService.GetAll();

            if (items.Count == 0)
            {
                return Ok("No objects available at the moment");
            }

            return Ok(items);
        }

        /// <summary>
        /// Download file with Name specified in Db
        /// </summary>
        [Authorize(RoleConst.UserAdmin)]
        [HttpGet("download")]
        public IActionResult Download(string fileName, string osType)
        {
            if (fileName == null || osType == null)
            {
                return BadRequest("osType is incorrect");
            }

            WebClient myWebClient = new WebClient();
            // Concatenate the domain with the Web resource filename.
            var myStringWebResource = pathServerStorage + fileName;
            // Download the Web resource and save it into the current filesystem folder.
            try
            {
                myWebClient.DownloadFile(myStringWebResource, fileName);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }



            return Ok();
        }

        /// <summary>
        /// Upload file with versioning
        /// </summary>
        /// <returns>id of uploaded file</returns>
        [Authorize(RoleConst.UserAdmin)]
        [HttpPost, DisableRequestSizeLimit, Route("file")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName);
            var fileNameWithoutExtension = file.FileName.Remove(file.FileName.Length - 4); // Folder Name
            var pathSaveFile = pathServerStorage + fileNameWithoutExtension;

            if (fileExtension == ".jpg" || fileExtension == ".png" || fileExtension  == ".bim" || fileExtension  == ".ifc")
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

                var pathForCreate = pathServerStorage + fileNameWithoutExtension + @"\" + fileNameWithoutExtension + " v" + lastVersion + fileExtension;

                using (var fstream = new FileInfo(pathForCreate).Create()) // Create instance to put an Object
                {
                    var c = fstream;
                    await file.CopyToAsync(fstream); // Put an Object
                }

                var itemModel = new ItemModel()
                {
                    Name = file.FileName + lastVersion,
                    RelativePath = pathForCreate,
                };
                var item = await _itemService.Create(itemModel); // Adding a Record about new Item
                return Ok(item);
            }
            else
            { return BadRequest(new { message = "invalid file format" });}
        }
    }
}

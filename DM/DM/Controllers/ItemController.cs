using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/item")]
    [Authorize]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private static string path = @$"E:\others\";
        //     private static string path = @$"K:\BrioMRS\ОБМЕН. Временные файлы\dm test files\"; Возможно постоянное использование этого пути

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        /// <summary>
        /// Get records about all documents
        /// </summary>
        /// <returns>list of items</returns>
        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _itemService.GetAll();

            return Ok(items);
        }

        /// <summary>
        /// Download file with Name specified in Db
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("download")]
        public IActionResult Download(string fileName)
        {
            WebClient myWebClient = new WebClient();
            // Concatenate the domain with the Web resource filename.
            var myStringWebResource = path + fileName;
            // Download the Web resource and save it into the current filesystem folder.
            myWebClient.DownloadFile(myStringWebResource, fileName);

            return Ok();
        }

        /// <summary>
        /// Upload file
        /// </summary>
        /// <param name="file"></param>
        /// <returns>id of uploaded file</returns>
        [Authorize]
        [HttpPost, DisableRequestSizeLimit, Route("file")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName);

            if (fileExtension == ".jpg" || fileExtension == ".png" || fileExtension  == ".bim" || fileExtension  == "ifc")
            {
                using (var fstream = new FileInfo(path + file.FileName).Create())
                {
                    file.CopyTo(fstream);
                }

                var itemModel = new ItemModel()
                {
                    Name = file.FileName,
                    RelativePath = path + file.FileName,
                };
                var item = await _itemService.Create(itemModel);
                return Ok(item);
            }
            else
            { return BadRequest(new { message = "invalid file format" });}
        }
    }
}

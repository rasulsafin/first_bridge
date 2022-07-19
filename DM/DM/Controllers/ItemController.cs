using DM.Domain.Interfaces;
using DM.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/item")]
    [Authorize(Roles ="admin")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private static string path = @$"K:\BrioMRS\ОБМЕН. Временные файлы\dm test files\";

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _itemService.GetAll();

            return Ok(items);
        }
        /*
        [HttpGet("{itemId}")]
        public IActionResult GetById(long itemId)
        {
            var item = _itemService.GetById(itemId);

            return Ok(item);
        }
        */
        /*
        [HttpGet("download")]
        public FileResult Download(string fileName)
        {
            var filepath = path + fileName;
            byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        */
        [Authorize]
        [HttpGet("download")]
        public IActionResult Download(string fileName)
        {
     /*       string remoteUri = "http://www.brio-dm.ru/library/"; */

            WebClient myWebClient = new WebClient();
            // Concatenate the domain with the Web resource filename.
            var myStringWebResource = path + fileName;
            // Download the Web resource and save it into the current filesystem folder.
            myWebClient.DownloadFile(myStringWebResource, fileName);

            return Ok();
        }
        [Authorize]
        [HttpPost, DisableRequestSizeLimit, Route("file")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            using (var fstream = new FileInfo(path + file.FileName).Create())
            {
                file.CopyTo(fstream);
            }

            var itemModel = new ItemModel()
            {
                Name = file.Name,
                RelativePath = path + file.FileName,
            };

            var item = await _itemService.Create(itemModel);
            return Ok(item);
        }
    }
}

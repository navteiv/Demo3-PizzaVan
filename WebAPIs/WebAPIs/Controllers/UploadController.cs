using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        public UploadController() { }
        //[HttpPost]
        //public async Task<IActionResult> Upload()
        //{
        //    var formCollection = await Request.ReadFormAsync();
        //    var files = formCollection.Files;
        //    foreach (var file in files)
        //    {
        //        var blobContainerClient = new BlobContainerClient("UseDevelopmentStorage=true", "images/Dish");
        //        blobContainerClient.CreateIfNotExists();
        //        var containerClient = blobContainerClient.GetBlobClient(file.FileName);
        //        var blobHttpHeader = new BlobHttpHeaders
        //        {
        //            ContentType = file.ContentType
        //        };
        //        await containerClient.UploadAsync(file.OpenReadStream(), blobHttpHeader);
        //    }

        //    return Ok();
        //}
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Image", "Dish");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}

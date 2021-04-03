using System;
using System.IO;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public FileUploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] FileUpload fileUpload)

        {
            Console.WriteLine(_environment.WebRootPath);
            try
            {
                if (fileUpload.Files.Length > 0)
                {
                    string path = _environment.WebRootPath + "/uploads/";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    using (FileStream fileStream = System.IO.File.Create(path + fileUpload.Files.FileName))
                    {
                        await fileUpload.Files.CopyToAsync(fileStream);
                        fileStream.Flush();
                        return Ok(new {url = path + fileUpload.Files.FileName});
                    }
                }
                else
                {
                    return BadRequest(new {message = "Can not  upload"});
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new {message = e.Message});
            }
        }
    }
}
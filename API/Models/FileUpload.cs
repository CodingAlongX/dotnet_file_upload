using Microsoft.AspNetCore.Http;

namespace API.Models
{
    public class FileUpload
    {
        public IFormFile Files { get; set; }
    }
}
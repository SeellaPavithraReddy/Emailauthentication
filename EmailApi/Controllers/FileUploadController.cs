using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailApi.Models.Context;
using EmailApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace EmailApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly EmailDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public FileUploadController(EmailDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file) // Removed [FromForm]
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // Specify the path where you want to save the uploaded files
            var uploadsFolderPath = @"C:\Users\pavithras\OneDrive - MOURI Tech\Desktop\Uploads";
            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            var filePath = Path.Combine(uploadsFolderPath, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var uploadedFile = new UploadedFile
            {
                FileName = file.FileName,
                FilePath = filePath
            };

            _context.uploadedFiles.Add(uploadedFile);
            await _context.SaveChangesAsync();

            return Ok(new { uploadedFile.Id, uploadedFile.FileName, uploadedFile.FilePath });
        }
    }
}

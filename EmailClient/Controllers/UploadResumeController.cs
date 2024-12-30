using System;
using System.IO;
using System.Net.Http;
using EmailClient.Models.ApiServices;
using EmailClient.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmailClient.Controllers
{
    [Route("[controller]")]
    public class UploadResumeController : Controller
    {
        private readonly UploadFileApiServices uploadFileApiServices;
        private readonly ILogger<UploadResumeController> logger;

        public UploadResumeController(UploadFileApiServices uploadFileApiServices, ILogger<UploadResumeController> logger)
        {
            this.uploadFileApiServices = uploadFileApiServices;
            this.logger = logger;
        }

        [HttpGet("Upload")]
        public IActionResult UploadFile()
        {
            return View(new UploadedFile());
        }

        [HttpPost("Upload")]
        public IActionResult UploadFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine("wwwroot/uploads", file.FileName);

                // Ensure the directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Call your API service to save the file reference
                try
                {
                    HttpResponseMessage httpResponseMessage = uploadFileApiServices.UploadFile(file);
                    string output = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        ViewBag.msg = "File uploaded successfully";
                    }
                    else
                    {
                        logger.LogError($"File upload failed: {httpResponseMessage.StatusCode} - {output}");
                        ViewBag.msg = $"File not uploaded: {httpResponseMessage.StatusCode} - {output}";
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError($"Exception during file upload: {ex.Message}");
                    ViewBag.msg = $"File not uploaded: {ex.Message}";
                }
            }
            else
            {
                ViewBag.msg = "No file selected";
            }

            return View();
        }
    }
}

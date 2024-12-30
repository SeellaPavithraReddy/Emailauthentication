// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using EmailApi.Models.DAO;
// using EmailApi.Models.Entities;
// using Microsoft.AspNetCore.Mvc;

// namespace EmailApi.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class FileController : ControllerBase
//     {

//     private readonly FileDao fileDao1;
//     private readonly string _fileStoragePath;

//     public FileController(FileDao fileDao)
//     {
//         fileDao1 = fileDao;
//         _fileStoragePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

//         if (!Directory.Exists(_fileStoragePath))
//         {
//             Directory.CreateDirectory(_fileStoragePath);
//         }
//     }

//     [HttpPost("upload")]
//     public IActionResult UploadFile(IFormFile file)
//     {
//         if (file == null || file.Length == 0)
//             return BadRequest("No file uploaded.");

//         var filePath = Path.Combine(_fileStoragePath, file.FileName);

//         using (var stream = new FileStream(filePath, FileMode.Create))
//         {
//             file.CopyTo(stream);
//         }

//         var fileModel = new FileModel
//         {
//             FileName = file.FileName,
//             FileContent = System.IO.File.ReadAllBytes(filePath),
//             FilePath = filePath
//         };
//         fileDao1.SaveFile(fileModel);

//         return Ok("File uploaded successfully.");
//     }

//     [HttpGet("download/{id}")]
//     public IActionResult DownloadFile(int id)
//     {
//         var file = fileDao1.GetFile(id);
//         if (file == null)
//             return NotFound();

//         return File(file.FileContent, "application/octet-stream", file.FileName);
//     }
// }

//     }

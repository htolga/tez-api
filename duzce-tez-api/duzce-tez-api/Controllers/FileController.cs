using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Core.Services.Interfaces;
using Domain.Domains;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace duzce_tez_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        public IWebHostEnvironment environment;
        public IFileService fileService;

        public FileController(IWebHostEnvironment _environment, IFileService _fileService)
        {
            environment = _environment;
            fileService = _fileService;
        }




        //Sisteme doküman eklenmesini sağlayan endpoint
        [HttpPost("AddDocument/{lessonId}")]
        public async Task<IActionResult> AddDocument(List<IFormFile> files, int lessonId)
        {
            string path = environment.WebRootPath;


            await fileService.DocumentUpload(files, lessonId, path);

            return Ok(new { message = "Done" });

        }

        //Ekrana dökümanların listelenmesini sağlayan endpoint
        [HttpGet("GetDocumentsByLessonId/{lessonId}")]
        public async Task<List<LessonDocument>> GetDocumentsByLessonId(int lessonId)
        {
            return await fileService.GetDocumentsByLessonId(lessonId);
        }


        //Ekrandan tıklanan dokümanın indirilmesini sağlayan endpoint
        [HttpGet("DownloadDocumentById/{id}")]
        public async Task<IActionResult> DownloadDocumentById(Guid id)
        {

            var document = await fileService.GetDocumentById(id);


            byte[] fileBytes = System.IO.File.ReadAllBytes(document.FilePath);


            return File(fileBytes, "application/force-download", document.FileName);
        }

        [HttpDelete("DeleteDocumentById/{id}")]
        public async Task<IActionResult> DeleteDocumentById(Guid id)
        {
            await fileService.DeleteDocumentById(id);

            return Ok(new { message = "Done!" });
        }








    }
}
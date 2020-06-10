using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Core.Services.Interfaces;
using Domain.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace duzce_tez_api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    public class HomeworkController : ControllerBase
    {
        public IWebHostEnvironment environment;
        public IFileService fileService;
        public HomeworkController(IWebHostEnvironment _env, IFileService _fileService)
        {
            environment = _env;
            fileService = _fileService;
        }

        [HttpPost("AddHomeworkToLesson")]
        public async Task<IActionResult> AddHomeWorkToLesson(LessonHomework lessonHomework)
        {
            await fileService.HomeworkAddToLesson(lessonHomework);
            return Ok(new { message = "Done!" });
        }

        [HttpPut("UpdateHomeworkById/{homeworkId}")]
        public async Task<LessonHomework> UpdateHomeworkById(int homeworkId, LessonHomework lessonHomework)
        {
            return await fileService.UpdateHomeworkById(homeworkId, lessonHomework);
        }

        [HttpDelete("DeleteHomeworkById/{homeworkId}")]
        public async Task<IActionResult> DeleteHomeworkById(int homeworkId)
        {
            await fileService.DeleteHomeworkById(homeworkId);

            return Ok(new {message = "Done!" });
        }

        [HttpPost("AddDocumentToHomeWork/{homeworkId}")]
        public async Task<IActionResult> AddDocumentToHomework(List<IFormFile> files, int homeworkId)
        {
            string path = environment.WebRootPath;
            await fileService.DocumentUploadToHomeWork(files, homeworkId, path);
            return Ok(new { message = "Done!" });
        }

        [HttpDelete("DeleteHomeworkDocument/{documentId}")]
        public async Task<IActionResult> DeleteHomeworkDocument(Guid documentId)
        {
            await fileService.DeleteHomeworkDocument(documentId);

            return Ok(new { message = "Done!" });
        }

        [HttpGet("GetHomeworkById/{homeworkId}")]
        public async Task<LessonHomework> GetHomeworkById(int homeworkId)
        {
            return await fileService.GetHomeworkById(homeworkId);
        }

        [HttpGet("GetHomeworkByLessonId/{lessonId}")]
        public async Task<List<LessonHomework>> GetHomeworkByLessonId(int lessonId)
        {
            return await fileService.GetHomeworksLessonId(lessonId);
        }

        [HttpGet("GetHomeworkDocumentByHomeworkId/{homeworkId}")]
        public async Task<List<HomeworkDocument>> GetHomeworkDocument(int homeworkId)
        {
            return await fileService.GetHomeworkDocumentByHomeworkId(homeworkId);
        }


        // Ödeve bağlı doküman indiriliyor
        [HttpGet("DownloadHomeworkDocumentById/{id}")]
        public async Task<IActionResult> DownloadHomeworkDocumentById(Guid id)
        {

            var homeworkDocument = await fileService.GetHomeworkDocumentById(id);


            byte[] fileBytes = System.IO.File.ReadAllBytes(homeworkDocument.FilePath);


            return File(fileBytes, "application/force-download", homeworkDocument.FileName);
        }

        [HttpPost("AddHomeworkToHomework/{homeworkId}/{studentId}")]
        public async Task<IActionResult> AddHomeworkToHomework(List<IFormFile> files, int homeworkId, int studentId)
        {
            string path = environment.WebRootPath;

            await fileService.HomeworkUploadToHomework(files, homeworkId, studentId, path);

            return Ok(new { message = "Done!" });
        }


        [HttpGet("GetAllStudentHomeworkByHomeworkId/{homeworkId}")]
        public async Task<List<StudentHomework>> GetAllStudentHomeworkByHomeworkId(int homeworkId)
        {
            return await fileService.GetAllStudentHomeworkById(homeworkId);
        }

        [HttpGet("DowloadHomeworkByStudentHomeworkId/{studentHomeworkId}")]
        public async Task<IActionResult> DownloadHomeworkByStudenthomeworkId(Guid studentHomeworkId)
        {
            var studentHomework = await fileService.HomeworkDownloadHomeworkByTeacher(studentHomeworkId);


            byte[] fileBytes = System.IO.File.ReadAllBytes(studentHomework.FilePath);


            return File(fileBytes, "application/force-download", studentHomework.FileName);
        }

        [HttpGet("CheckStudentHomework/{homeworkId}/{studentId}")]
        public async Task<string> CheckStudentHomework(int homeworkId, int studentId)
        {
            var result = await fileService.CheckStudentHomework(homeworkId, studentId);
            return result;
        }


    }
}
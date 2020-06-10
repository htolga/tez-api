using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Dtos.Lesson;
using Common.Dtos.User;
using Core.Services.Interfaces;
using Domain.Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace duzce_tez_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService lessonService;

        public LessonController(ILessonService _lessonService)
        {
            lessonService = _lessonService;
        }

        [HttpGet("GetAllLesson")]
        public async Task<IActionResult> GetAllLesson()
        {
            var list = await lessonService.GetAllLesson();

            return new JsonResult(list);
        }

        [HttpGet("GetLessonById/{lessonId}")]
        public async Task<LessonDto> GetLessonById(int lessonId)
        {
            return await lessonService.GetLessonById(lessonId);
        }

        

        [HttpPost("AddLesson")]
        public async Task<string> AddLesson(LessonDto data)
        {
            await lessonService.AddLesson(data);

            return "Done!";
        }

        [HttpDelete("DeleteLesson/{lessonId}")]
        public async Task<string> DeleteLesson(int lessonId)
        {
            await lessonService.DeleteLesson(lessonId);

            return "Done!";
        }

        [HttpPut("UpdateLesson/{lessonId}")]
        public async Task<string> UpdateLesson(int lessonId, LessonDto data)
        {

            await lessonService.UpdateLesson(lessonId, data);

            return "Done!";
        }



        [HttpGet("GetStudentByLesson/{lessonId}")]
        public async Task<IActionResult> GetStudentLessonsById(int lessonId)
        {
            var list = await lessonService.GetAllStudentByLessonId(lessonId);

            return new JsonResult(list);
        }

        [HttpGet("GetTeacherByLesson/{lessonId}")]
        public async Task<UserDto> GetTeacherByLesson(int lessonId)
        {
            
            var teacher = await lessonService.GetTeacherByLesson(lessonId);
            
            return teacher;
        }

        [HttpPost("AddUserToLesson/{lessonId}/{userId}")]
        public async Task<string> AddUserToLesson(int lessonId, int userId)
        {
            await lessonService.AddUserToLesson(lessonId, userId);

            return "Done!";
            
        }

        [HttpDelete("DeleteUserInLesson/{lessonId}/{userId}")]
        public async Task<string> DeleteUserInLesson(int lessonId, int userId)
        {
            await lessonService.DeleteUserInLesson(lessonId, userId);

            return "Done!";
        }

        [HttpGet("GetLessonByUserId/{userId}")]
        public async Task<List<LessonDto>> GetLessonByUserId(int userId)
        {
            return await lessonService.GetLessonByUserId(userId);
        }

        [HttpGet("GetLessonWithoutTeacher")]
        public async Task<List<LessonDto>> GetLessonWithoutTeacher()
        {
            return await lessonService.GetLessonWithoutTeacher();
        }

        [HttpGet("GetNotOwnedLessonsByUserId/{userId}")]
        public async Task<List<LessonDto>> GetNotOwnedLessonsByUserId(int userId)
        {
            return await lessonService.GetNotOwnedLessonsByUserId(userId);
        }
    }
}
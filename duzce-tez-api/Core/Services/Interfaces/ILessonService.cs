using Common.Dtos;
using Common.Dtos.Lesson;
using Common.Dtos.User;
using Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface ILessonService
    {
        Task<IEnumerable<LessonDto>> GetAllLesson();

        Task<LessonDto> GetLessonById(int lessonId);

        Task AddLesson(LessonDto data);

        Task DeleteLesson(int LessonId);

        Task UpdateLesson(int LessonId,LessonDto data);

        Task<IEnumerable<StudentDto>> GetAllStudentByLessonId(int lessonId);

        Task<UserDto> GetTeacherByLesson(int lessonId);

        Task<List<LessonDto>> GetLessonByUserId(int userId);

        Task<List<LessonDto>> GetNotOwnedLessonsByUserId(int userId);

        Task<List<LessonDto>> GetLessonWithoutTeacher();

        Task AddUserToLesson(int lessonId, int userId);

        Task DeleteUserInLesson(int lessonId, int userId);
        
    }
}

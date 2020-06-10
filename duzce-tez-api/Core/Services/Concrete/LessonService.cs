using Common.Consts;
using Common.Dtos;
using Common.Dtos.Lesson;
using Common.Dtos.User;
using Common.Enums;
using Core.Services.Interfaces;
using Domain.Domains;
using Domain.Domains.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Services
{
    public class LessonService : ILessonService
    {
        private readonly CurrentContext ctx;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public LessonService(CurrentContext _ctx, UserManager<User> _userManager, RoleManager<Role> _roleManager)
        {
            ctx = _ctx;
            userManager = _userManager;
            roleManager = _roleManager;
        }

        public async Task AddLesson(LessonDto data)
        {
            Lesson lesson = new Lesson
            {
                Name = data.Name,
                Code = data.Code,
                Description = data.Description
            };

            ctx.Lessons.Add(lesson);
            await ctx.SaveChangesAsync();
        }

        public async Task DeleteLesson(int LessonId)
        {
            var lesson = await ctx.Lessons.FindAsync(LessonId);

            ctx.Lessons.Remove(lesson);

            await ctx.SaveChangesAsync();

        }

        public async Task<IEnumerable<LessonDto>> GetAllLesson()
        {
            var lessons = await ctx.Lessons.Select(x => new LessonDto { Id = x.Id, Name = x.Name, Description = x.Description, Code = x.Code }).ToListAsync();

            return lessons;
        }

        public async Task<LessonDto> GetLessonById(int lessonId)
        {
            var lesson =  await ctx.Lessons.FindAsync(lessonId);

            if (lesson == null) throw new Exception();

            return new LessonDto
            {
                Id = lesson.Id,
                Name = lesson.Name,
                Description = lesson.Description,
                Code = lesson.Code
            };
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentByLessonId(int lessonId)
        {
            //lessonId 1 ve fizik dersini ifade ediyor olsun. fizik dersine ait tüm öğretmen/öğrencileri getirir. hepsi user.
            var userIds = await ctx.UserLessons.Where(x => x.LessonId == lessonId).Select(x => x.UserId).ToListAsync();

            //öğrenci rolüne ait id alınır.
            var studentRoleId = roleManager.FindByNameAsync(RoleConst.STUDENT).Result.Id;

            //yukarıda gelen useridlerden rolü öğrenci olanların useridlerini getirir.
            var studentIds = ctx.UserRoles.Where(x => userIds.Contains(x.UserId) && x.RoleId == studentRoleId).Select(x => x.UserId).ToList();

            //gelen useridlere ait kişi bilgilerini döner.
            var students = ctx.Users.Where(x => studentIds.Contains(x.Id)).Select(x => new StudentDto
            {
                Id = x.Id,
                Name = x.Name,
                Lastname = x.Surname,
                Username = x.UserName
            });

            return students;
        }

        public async Task<UserDto> GetTeacherByLesson(int lessonId)
        {
            var userIds = await ctx.UserLessons.Where(x => x.LessonId == lessonId).Select(x => x.UserId).ToListAsync();

            var teacherRoleId = roleManager.FindByNameAsync(RoleConst.TEACHER).Result.Id;

            var teacherEntity = ctx.UserRoles.Where(x => userIds.Contains(x.UserId) && x.RoleId == teacherRoleId).FirstOrDefault();

            if(teacherEntity == null)
            {
                return null;
            }

            var teacher = ctx.Users.FirstOrDefault(x => x.Id == teacherEntity.UserId);

            

            return new UserDto { 
                Id = teacher.Id,
                Name = teacher.Name,
                Surname = teacher.Surname
            };
        }



        public async Task UpdateLesson(int LessonId, LessonDto data)
        {

            var lesson = await ctx.Lessons.FindAsync(LessonId);

            if (lesson == null) throw new Exception();

            lesson.Name = data.Name;
            lesson.Code = data.Code;
            lesson.Description = data.Description;

            await ctx.SaveChangesAsync();

        }

        

        public async Task AddUserToLesson(int lessonId, int userId)
        {
            var userLesson = new UserLesson
            {
                LessonId = lessonId,
                UserId = userId
            };
            ctx.UserLessons.Add(userLesson);
            await ctx.SaveChangesAsync();
        }

        public async Task DeleteUserInLesson(int lessonId, int userId)
        {
            var userLesson =  ctx.UserLessons.FirstOrDefault(x => x.LessonId == lessonId && x.UserId == userId);

            ctx.UserLessons.Remove(userLesson);

            await ctx.SaveChangesAsync();
        }


        public async Task<List<LessonDto>> GetLessonByUserId(int userId)
        {
            var lessonsId = await ctx.UserLessons.Where(x => x.UserId == userId).Select(x => x.LessonId).ToListAsync();

            var lessons = ctx.Lessons.Where(x => lessonsId.Contains(x.Id)).Select(x => new LessonDto
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code
            });

            return lessons.ToList();

        }

        public async Task<List<LessonDto>> GetNotOwnedLessonsByUserId(int userId)
        {
            var lessonsId = await ctx.UserLessons.Where(x => x.UserId == userId).Select(x => x.LessonId).ToListAsync();

            var lessons = ctx.Lessons.Where(x => !lessonsId.Contains(x.Id)).Select(x => new LessonDto
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code
            });

            return lessons.ToList();

        }

        public async Task<List<LessonDto>> GetLessonWithoutTeacher()
        {
            var teacherRoleId = roleManager.FindByNameAsync(RoleConst.TEACHER).Result.Id;

            var teacherIds = await ctx.UserRoles.Where(x => x.RoleId == teacherRoleId).Select(x => x.UserId).ToArrayAsync();

            var lessonIds = ctx.UserLessons.Where(x => teacherIds.Contains(x.UserId)).Select(x => x.LessonId).ToList();

            var lessons = ctx.Lessons.Where(x => !lessonIds.Contains(x.Id)).Select(x => new LessonDto { 
                
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
            });

            return lessons.ToList();
        }
    }
}

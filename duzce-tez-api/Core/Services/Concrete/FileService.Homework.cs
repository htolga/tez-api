using Domain.Domains;
using Domain.Domains.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Concrete
{
    public partial class FileService
    {
        
        public async Task<string> HomeworkAddToLesson(LessonHomework lessonHomework)
        {
            var tempLessonHomework = new LessonHomework
            {
                Deadline = lessonHomework.Deadline,
                Name = lessonHomework.Name,
                Description = lessonHomework.Description,
                LessonId = lessonHomework.LessonId
            };
            ctx.lessonHomeworks.Add(tempLessonHomework);

            await ctx.SaveChangesAsync();
            return "Homework Upload Success";
        }

        public async Task<LessonHomework> UpdateHomeworkById(int homeworkId, LessonHomework lessonHomework)
        {
            var homework = await ctx.lessonHomeworks.FindAsync(homeworkId);

            if(homework != null)
            {
                homework.Name = lessonHomework.Name;
                homework.Description = lessonHomework.Description;
                homework.Deadline = lessonHomework.Deadline;

                await ctx.SaveChangesAsync();

                return lessonHomework;
            }

            throw new Exception();
        }

        public async Task DeleteHomeworkById(int homeworkId)
        {
            var homework = await ctx.lessonHomeworks.FindAsync(homeworkId);

            if(homework != null)
            {
               ctx.lessonHomeworks.Remove(homework);

                await ctx.SaveChangesAsync();
            }

            
        }

        public async Task<LessonHomework> GetHomeworkById(int homeworkId)
        {
            var homework = await ctx.lessonHomeworks.FindAsync(homeworkId);

            return homework;
        }

        //Öğrenci yüklenen ödevleri listeliyor.
        public async Task<List<LessonHomework>> GetHomeworksLessonId(int lessonId)
        {
            var homeworks = await ctx.lessonHomeworks.Where(item => item.LessonId == lessonId).ToListAsync();
            return homeworks;
        }

        public async Task<List<HomeworkDocument>> GetHomeworkDocumentByHomeworkId(int homeworkId)
        {
            var homeworkDocuments = await ctx.HomeworkDocuments.Where(item => item.HomeworkId == homeworkId).ToListAsync();

            return homeworkDocuments;
        }

        public async Task<HomeworkDocument> GetHomeworkDocumentById(Guid id)
        {
            var homeworkDocument = await ctx.HomeworkDocuments.Where(item => item.FileKey == id).FirstAsync();

            return homeworkDocument;
        }

        //öğretmen verdiği ödeve döküman yükleyecek
        public async Task<string> DocumentUploadToHomeWork(List<IFormFile> files, int homeworkId, string path)
        {
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filename = formFile.FileName;
                    var guidKey = Guid.NewGuid();

                    if (!Directory.Exists(path + "\\Upload\\Homeworks\\"))
                    {
                        Directory.CreateDirectory(path + "\\Upload\\Homeworks\\");
                    }
                    var extension = formFile.FileName.Split(".")[1];

                    var fullPath = path + "\\Upload\\Homeworks\\" + guidKey + "." + extension;

                    using (var stream = System.IO.File.Create(fullPath))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    var homeworkDocument  = new HomeworkDocument()
                    {
                        HomeworkId = homeworkId,
                        FileKey = guidKey,
                        FileName = filename,
                        FilePath = fullPath
                    };

                    ctx.HomeworkDocuments.Add(homeworkDocument);
                }

            }
            await ctx.SaveChangesAsync();
            return "Upload Successyfully";
        }

        // Ödeve ait dokümanın silinmesi.
        public async Task DeleteHomeworkDocument(Guid documentId)
        {
            var document = await ctx.HomeworkDocuments.Where(x => x.FileKey == documentId).FirstAsync();

            if (document != null)
            {
                File.Delete(document.FilePath);
                ctx.Remove(document);
                await ctx.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Doküman silinemedi");
            }
        }


        //öğrenci ödevini yüklüyor.
        public async Task<string> HomeworkUploadToHomework(List<IFormFile> files, int homeworkId, int userId, string path)
        {
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filename = formFile.FileName;
                    var guidKey = Guid.NewGuid();

                    if (!Directory.Exists(path + "\\Upload\\Homeworks\\"))
                    {
                        Directory.CreateDirectory(path + "\\Upload\\Homeworks\\");
                    }
                    var extension = formFile.FileName.Split(".")[1];

                    var fullPath = path + "\\Upload\\Homeworks\\" + guidKey + "." + extension;

                    using (var stream = System.IO.File.Create(fullPath))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    var studentHomework = new StudentHomework()
                    {
                        LessonHomeworkId = homeworkId,
                        UserId = userId,
                        FileKey = guidKey,
                        FileName = filename,
                        FilePath = fullPath
                    };

                    ctx.StudentHomeworks.Add(studentHomework);
                }

            }
            await ctx.SaveChangesAsync();
            return "Upload Successyfully";
        }

        public async Task<List<StudentHomework>> GetAllStudentHomeworkById(int homeworkId)
        {
            var studentHomeworks = await ctx.StudentHomeworks.Where(item => item.LessonHomeworkId == homeworkId).ToListAsync();

            return studentHomeworks;
        }

        public async Task<string> CheckStudentHomework(int homeworkId, int studentId)
        {
            var result = await ctx.StudentHomeworks.Where(x => x.LessonHomeworkId == homeworkId && x.UserId == studentId).FirstOrDefaultAsync();

            if(result != null)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }



        public async Task<StudentHomework> HomeworkDownloadHomeworkByTeacher(Guid studentHomeworkId)
        {
            var studentHomework = await ctx.StudentHomeworks.Where(item => item.FileKey == studentHomeworkId).FirstAsync();
            return studentHomework;
        }


    }
}

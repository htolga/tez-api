using Domain.Domains;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IFileService
    {
       
        //Öğretmen üzerindeki derse ödev yüklüyor.
        Task<string> HomeworkAddToLesson(LessonHomework lessonHomework);

        // Id ile ödev çağırma
        Task<LessonHomework> GetHomeworkById(int homeworkId);

        // Id ile ödev güncelleme
        Task<LessonHomework> UpdateHomeworkById(int homeworkId, LessonHomework lessonHomework);

        // Ödev id'si ile ödev silme
        Task DeleteHomeworkById(int homeworkId);


        //Ödeve dosya ekleme
        Task<string> DocumentUploadToHomeWork(List<IFormFile> files, int homeworkId, string path);

        // Ödeve Eklenmiş Dokümanı silme
        Task DeleteHomeworkDocument(Guid documentId);

        //Öğrenci Derse bağlı ödevleri listeliyor
        Task<List<LessonHomework>> GetHomeworksLessonId(int lessonId);

        //Öğrenci Ödeve bağlı dökümanları listeliyor.
        Task<List<HomeworkDocument>> GetHomeworkDocumentByHomeworkId(int homeworkId);

        //Öğrenci ödeve bağlı dökümanı id ile okuyor.
        Task<HomeworkDocument> GetHomeworkDocumentById(Guid id);

        //Öğrenci üzerindeki derse atanmış ödeve ödev atıyor.
        Task<string> HomeworkUploadToHomework(List<IFormFile> files, int homeworkId,int userId,string path);

        // Öğretmen öğrencilerin gönderdiği ödevleri listeliyor.
        Task<List<StudentHomework>> GetAllStudentHomeworkById(int homeworkId);

        // Öğrenci Ödev göndermiş mi kontrol ediyoruz.
        Task<string> CheckStudentHomework(int homeworkId, int studentId); 

        
        //öğretmen öğrencinin ödevini indirir
        Task<StudentHomework> HomeworkDownloadHomeworkByTeacher(Guid studentHomeworkId);



        




        //Ders dokümanı yükleme
        #region Documents 
        Task<string> DocumentUpload(List<IFormFile> files, int lessonId, string path);

        //Derse ait dökümanları listeleme
        Task<List<LessonDocument>> GetDocumentsByLessonId(int lessonId);

        Task<LessonDocument> GetDocumentById(Guid id);

        Task DeleteDocumentById(Guid id);
        #endregion


    }
}

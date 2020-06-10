using Core.Services.Interfaces;
using Domain.Domains;
using Domain.Domains.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Concrete
{
    public partial class FileService : IFileService
    {
        private readonly CurrentContext ctx;

        public FileService(CurrentContext _ctx)
        {
            ctx = _ctx;
        }

        public async Task<List<LessonDocument>>  GetDocumentsByLessonId(int lessonId)
        {

            var documents = await ctx.LessonDocuments.Where(x => x.LessonId == lessonId).ToListAsync();
            

            return documents;
        }

        public async Task DeleteDocumentById(Guid id)
        {
            var document = await ctx.LessonDocuments.Where(x => x.FileKey == id).FirstAsync();

            if(document != null)
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

        public async Task<LessonDocument> GetDocumentById(Guid id)
        {
           
            var document = await ctx.LessonDocuments.Where(x => x.FileKey == id).FirstAsync();

            return document;
        }

        public async Task<string> DocumentUpload(List<IFormFile> files, int lessonId, string path)
        {
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filename = formFile.FileName;
                    var guidKey = Guid.NewGuid();

                    if (!Directory.Exists(path + "\\Upload\\Documents\\"))
                    {
                        Directory.CreateDirectory(path + "\\Upload\\Documents\\");
                    }
                    var extension = formFile.FileName.Split(".")[1];

                    var fullPath = path + "\\Upload\\Documents\\" + guidKey + "." + extension;

                    using (var stream = System.IO.File.Create(fullPath))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    var lessonDocument = new LessonDocument()
                    {
                        LessonId = lessonId,
                        FileKey = guidKey,
                        FileName = filename,
                        FilePath = fullPath
                    };

                    ctx.LessonDocuments.Add(lessonDocument);
                }

            }
            await ctx.SaveChangesAsync();
            return "Upload Successyfully";
        }
    }
}

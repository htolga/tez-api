using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Domains
{
    [Table("LessonDocument", Schema = "Lesson")]
    public class LessonDocument : EntityBase
    {
        public Guid FileKey { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
        

    }
}

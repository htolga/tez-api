using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Domains
{
    [Table("HomeworkDocument", Schema = "Lesson")]
    public class HomeworkDocument : EntityBase
    {
        public Guid FileKey { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int HomeworkId { get; set; }
        public virtual LessonHomework Homework { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Domains
{
    public  class StudentHomework : EntityBase
    {
        public Guid FileKey { get; set; }
        public string FilePath { get; set; }

        public string FileName { get; set; }

        public int LessonHomeworkId { get; set; }

        public virtual LessonHomework LessonHomework { get; set; }

        public int UserId { get; set; }

        public virtual  User User { get; set; }
    }
}

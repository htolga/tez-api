using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Domains
{
    [Table("UserLesson", Schema = "Lesson")]
    public class UserLesson : EntityBase
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}

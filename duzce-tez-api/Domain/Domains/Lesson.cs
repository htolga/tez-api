using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Domains
{
    [Table("Lesson", Schema = "Lesson")]
    public class Lesson : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }

        public ICollection<UserLesson> UserLessons { get; set; }
    }
}

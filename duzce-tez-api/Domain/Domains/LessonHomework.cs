using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Domains
{
   public class LessonHomework : EntityBase
    {
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Deadline { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}

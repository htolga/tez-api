using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dtos.Lesson
{
    public class LessonDto:BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public List<int> UserIds { get; set; }
    }
}

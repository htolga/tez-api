using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dtos
{
    public class StudentDto : BaseDto
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
    }
}

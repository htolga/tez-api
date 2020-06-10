using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dtos
{
    public class BaseDto
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

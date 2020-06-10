using Domain.Domains;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Domains
{
    [Table("Users", Schema = "User")]
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string TCKN { get; set; }

        public ICollection<UserLesson> UserLessons { get; set; }
    }
}

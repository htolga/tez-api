using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Domains
{
    [Table("Roles",Schema="User")]
    public class Role:IdentityRole<int>
    {
        public Role()
        {

        }
        public Role(string name)
        {
            Name = name;
        }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}

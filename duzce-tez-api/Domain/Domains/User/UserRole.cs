using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Domains
{
    [Table("UserRoles", Schema = "User")]
    public class UserRole : IdentityUserRole<int>
    {
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        //public virtual User User { get; set; }
        //public virtual Role Role { get; set; }
    }
}

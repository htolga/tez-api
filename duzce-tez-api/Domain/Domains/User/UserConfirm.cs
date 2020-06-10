using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Domains
{
    [Table("UserConfirms", Schema = "User")]
    public class UserConfirm:EntityBase
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Token { get; set; }
        public bool IsConfirmed { get; set; }

    }
}

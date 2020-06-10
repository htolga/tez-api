using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Domains
{
    [Table("UserLogins", Schema = "User")]
    public class UserLogin:IdentityUserLogin<int> { }
}

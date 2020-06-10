
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Domains
{
    [Table("UserClaims", Schema = "User")]
    public class UserClaim : IdentityUserClaim<int> { }

    [Table("RoleClaims", Schema = "User")]
    public class RoleClaim : IdentityRoleClaim<int> { }

    [Table("UserTokens", Schema = "User")]
    public class UserToken : IdentityUserToken<int> { }
}

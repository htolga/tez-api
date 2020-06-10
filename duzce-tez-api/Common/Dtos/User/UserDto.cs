using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dtos.User
{
    public class UserDto : BaseDto
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName { get { return string.Format("{0} {1}", this.Name, this.Surname); } }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserRole { get; set; }
    }
}

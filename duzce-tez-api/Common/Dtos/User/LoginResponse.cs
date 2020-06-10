using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dtos.User
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}

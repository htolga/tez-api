using Common.Dtos.User;
using Domain.Domains;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public class IdentityHelper
    {
        private readonly UserManager<User> userManager;

        public IdentityHelper(UserManager<User> _userManager)
        {
            userManager = _userManager;
        }

        public async Task<UserDto> FindByNameAsync(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            return new UserDto
            {
                Name = user.Name,
                Surname = user.Surname,
                Username = user.UserName,
            };
        }

        public async Task<IdentityResult> AddUser(UserDto data)
        {
            var user = new User()
            {
                Name = data.Name,
                Surname = data.Surname,
                Email = data.Email,
                UserName = data.Username,
            };

            var result = await userManager.CreateAsync(user, data.Password);
            return result;
        }

    }
}

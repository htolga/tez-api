using Common.Dtos.User;
using Core.Services.Interfaces;
using Domain.Domains;
using Domain.Domains.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly CurrentContext ctx;

        public UserService(UserManager<User> _userManager, RoleManager<Role> _roleManager, CurrentContext _ctx, IConfiguration _conf)
        {
            configuration = _conf;
            userManager = _userManager;
            roleManager = _roleManager;
            ctx = _ctx;
        }

        public async Task<IdentityResult> AddUserAsync(UserDto data)
        {

            var user = new User()
            {
                Name = data.Name,
                Surname = data.Surname,
                Email = data.Email,
                Phone = data.Phone,
                UserName = data.Username,
               
            };

            var userResult = await userManager.CreateAsync(user, data.Password);

            if (userResult.Succeeded)
            {
                var roleResult = await userManager.AddToRoleAsync(user, data.UserRole);
                return roleResult;
            }

            return userResult;
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

        public async Task<UserDto> FindByIdAsync(int userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());

            var userRole = await ctx.UserRoles.FirstOrDefaultAsync(x => x.UserId == user.Id);

            var role = await ctx.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Username = user.UserName,
                Email = user.Email,
                Phone = user.Phone,
                UserRole = role.Name
            };
        }

        public async Task<IdentityResult> UpdateUser(UserDto data)
        {
            var user = await userManager.FindByIdAsync(data.Id.ToString());

            user.Name = data.Name;
            user.Surname = data.Surname;
            user.Email = data.Email;
            user.Phone = data.Phone;

            return await userManager.UpdateAsync(user);

        }

        public async Task<IdentityResult> DeleteUser(int user_Id)
        {
            var user = await userManager.FindByIdAsync(user_Id.ToString());


            return await userManager.DeleteAsync(user);
        }

        public async Task<List<UserDto>> GetAllUsersByRole(string roleName)
        {
            var results = await userManager.GetUsersInRoleAsync(roleName);
       
            return results.Select(x => new UserDto
            {
                Name = x.Name,
                Surname = x.Surname,
                Email = x.Email,
                Phone = x.Phone,
                UserRole = roleName,
                Id = x.Id
            }).ToList();
        }


        public async Task<LoginResponse> Authenticate(LoginRequest loginRequest)
        {
            User user = null;
            try
            {
               user = await userManager.FindByEmailAsync(loginRequest.Email);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            

            var userRole = await ctx.UserRoles.FirstOrDefaultAsync(x => x.UserId == user.Id);

            var role = await ctx.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);

            var passResult = await userManager.CheckPasswordAsync(user, loginRequest.Password);

            if (passResult)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, loginRequest.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var key =Encoding.UTF8.GetBytes(configuration["TokenOptions:SecurityKey"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                          new Claim(ClaimTypes.Email, loginRequest.Email),
                           new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                    Audience = configuration["TokenOptions:Audience"],
                    Issuer = configuration["TokenOptions:Issuer"],
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new LoginResponse
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Phone = user.Phone,
                    Role = role.Name,
                    Token = tokenHandler.WriteToken(token)
                };
            }

            throw new Exception();
        }

        
    }
}

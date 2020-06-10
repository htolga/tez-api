using Common.Dtos.User;
using Core.Helpers;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace duzce_tez_api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        [HttpPost("AddUser")]
        public async Task<IdentityResult> AddUserAsync(UserDto data)
        {
            return await userService.AddUserAsync(data); 
        }

        [HttpGet("GetUserById/{userId}")]
        public async Task<UserDto> GetUserById(int userId)
        {
            return await userService.FindByIdAsync(userId);
        }

        [HttpPut("UpdateUser")]
        public async Task<IdentityResult> UpdateUser(UserDto data)
        {
            return await userService.UpdateUser(data);
        }

        [HttpDelete("DeleteUser/{userId}")]
        public async Task<IdentityResult> DeleteUser(int userId)
        {
            return await userService.DeleteUser(userId);
        }

        //Kullanıcıları getir getir.
        // GetUsersByRoleName/student -- öğrencileri listeler
        // GetUserByRoleName/teacher -- öğretmenleri listeler
        [HttpGet("GetUsersByRoleName/{roleName}")]
        public async Task<List<UserDto>> GetUserByRoleName(string roleName)
        {
            return  await userService.GetAllUsersByRole(roleName);
        }

        [AllowAnonymous]
        [HttpPost("Authentication")]
        public async Task<LoginResponse> Authentication(LoginRequest loginRequest)
        {
            return await userService.Authenticate(loginRequest);
        }
    }
}

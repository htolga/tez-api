using Common.Dtos.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> AddUserAsync(UserDto data);
        Task<UserDto> FindByNameAsync(string username);
        Task<UserDto> FindByIdAsync(int userId);
        Task<IdentityResult> UpdateUser(UserDto user);
        Task<IdentityResult> DeleteUser(int user_Id);
        Task<List<UserDto>> GetAllUsersByRole(string roleName);
        Task<LoginResponse> Authenticate(LoginRequest loginRequest);
    }
}

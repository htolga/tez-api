using Common.Dtos.Teacher;
using Domain.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDto>> GetAllTeacher();

        Task AddTeacher(User data);

        Task DeleteTeacher(int TeacherId);

        Task UpdateTeacher(int TeacherId, User data);

    }
}

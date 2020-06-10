using Common.Dtos.Teacher;
using Core.Services.Interfaces;
using Domain.Domains;
using Domain.Domains.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Concrete
{

    public class TeacherService : ITeacherService
    {
        private readonly CurrentContext ctx;

        public TeacherService(CurrentContext _context)
        {
            ctx = _context;
        }


        public Task AddTeacher(User data)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTeacher(int TeacherId)
        {
            throw new NotImplementedException();
        }

        

        public Task UpdateTeacher(int TeacherId, User data)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<TeacherDto>> ITeacherService.GetAllTeacher()
        {
            throw new NotImplementedException();
        }
    }
}

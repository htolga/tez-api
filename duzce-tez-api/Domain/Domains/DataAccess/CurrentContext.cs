using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Domains.DataAccess
{
    public class CurrentContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        private readonly IConfiguration Configuration;
        public CurrentContext(DbContextOptions<CurrentContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<LessonDocument> LessonDocuments { get; set; }
        public DbSet<UserLesson> UserLessons { get; set; }
        public DbSet<LessonHomework> lessonHomeworks { get; set; }
        public DbSet<StudentHomework> StudentHomeworks { get; set; }

        public DbSet<HomeworkDocument> HomeworkDocuments { get; set; }
    }
}


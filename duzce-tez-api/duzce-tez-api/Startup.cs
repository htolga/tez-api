using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Services;
using Core.Services.Concrete;
using Core.Services.Interfaces;
using Domain.Domains;
using Domain.Domains.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace duzce_tez_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
                });
            });
            services.AddControllers();

            string connectionStr = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<CurrentContext>(options =>
            {
                options.UseSqlServer(connectionStr);
            });

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<CurrentContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(opt =>
            {
                //password
                opt.Password.RequiredLength = 3;
                opt.Password.RequireDigit = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
                //lockout
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.AllowedForNewUsers = true;
                //user
                opt.User.RequireUniqueEmail = true;
            });

            var key = Encoding.UTF8.GetBytes(Configuration["TokenOptions:SecurityKey"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["TokenOptions:Audience"],
                    ValidIssuer = Configuration["TokenOptions:Issuer"]

                };
            });

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidAudience = Configuration["TokenOptions:Audience"],
            //        ValidIssuer = Configuration["TokenOptions:Issuer"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenOptions:SecurityKey"])),
            //        ValidateIssuerSigningKey = true
            //    };
            //});



            services.AddTransient<ILessonService, LessonService>();
            services.AddTransient<ITeacherService, TeacherService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IFileService, FileService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



        }
    }
}

using AutoMapper;
using System.Reflection;
using Online_Learning_Management.Infrastructure.Repositories.Modules;
using Online_Learning_Management.Domain.Interfaces.Modules;
using Online_Learning_Management.Application.Modules.Services;
using Online_Learning_Management.Application.ModuleTasks.Services;
using Online_Learning_Management.Domain.Interfaces.ModuleTasks;
using Online_Learning_Management.Infrastructure.Repositories.ModuleTasks;
using Online_Learning_Management.Infrastructure.Repositories.Courses;
using Online_Learning_Management.Domain.Interfaces.GradeStudent;
using Online_Learning_Management.Application.GradeStudent.Services;
using Online_Learning_Management.Infrastructure.Repositories.GradeStudent;
using Online_Learning_Management.Domain.Interfaces.CourseStudents;
using Online_Learning_Management.Infrastructure.Repositories.CourseStudents;
using Online_Learning_Management.Domain.Interfaces.Forums;
using Online_Learning_Management.Infrastructure.Repositories.Forum;
using Online_Learning_Management.Application.Forums.Services;
using Online_Learning_Management.Domain.Interfaces.Students;
using Online_Learning_Management.Infrastructure.Students;
using Online_Learning_Management.Application.Students.Services;
using Online_Learning_Management.Domain.Interfaces.Files;
using Online_Learning_Management.Infrastructure.Repositories.Files;
using Online_Learning_Management.Application.Files.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Online_Learning_Management.Domain.Interfaces.Auth;
using Online_Learning_Management.Infrastructure.Repositories.Auth;
using Online_Learning_Management.Application.Auth.Services;
using Online_Learning_Management.Domain.Interfaces.ReportCourses;
using Online_Learning_Management.Infrastructure.Repositories.ReportCourses;
using Online_Learning_Management.Application.ReportCourses.Services;
using Online_Learning_Management.Application.TaskStudent.Services;
using Online_Learning_Management.Domain.Interfaces.TaskStudents;
using Online_Learning_Management.Infrastructure.Repositories.TaskStudents;
using Online_Learning_Management.Domain.Interfaces.Instructors;
using Online_Learning_Management.Infrastructure.Repositories.Instructors;
using Online_Learning_Management.Application.Instructors.Services;
using OnlineLearningManagement.Domain.Interfaces;
using Online_Learning_Management.Infrastructure.Repositories;
using Online_Learning_Management.Application.Modules.Services.ModuleProgresses;
using Online_Learning_Management.Domain.Interfaces.Post;
using Online_Learning_Management.Infrastructure.Repositories.Post;
using Online_Learning_Management.Application.Post.Services;

namespace Online_Learning_Management.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddProjectServices(this IServiceCollection services, IConfiguration configuration)
        {
          
            //Register repositories
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IModuleTaskRepository, ModuleTaskRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseStudentsRepository, CourseStudentsRepository>();
            services.AddScoped<IForumRepository, ForumRepository>();
            services.AddScoped<IGradeStudentRepository, GradeStudentRepository>();
            services.AddScoped<IStudentRepository , StudentRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IUserAuthRepository, UserAuthRepository>();
            services.AddScoped<IReportCourseRepository, ReportCourseRepository>();
            services.AddScoped<ITaskStudentRepository, TaskStudentRepository>();
            services.AddScoped<INstructorRepository, InstructorRepository>();
            services.AddScoped<IModuleProgressRepository, ModuleProgressesRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
          
            //Register services
            services.AddScoped<IModuleService, ModuleServices>();
            services.AddScoped<IModuleTaskService, ModuleTaskService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICourseStudentsService, CourseStudentsService>();
            services.AddScoped<IForumService, ForumService>();
            services.AddScoped<IGradeStudentService, GradeStudentService>();
            services.AddScoped<IStudentServices , StudentService>();
            services.AddScoped<IFileService, AzureBlobService>();
            services.AddScoped<IAuthUserService, UserService>();
            services.AddScoped<IAuthValidService, AuthService>();
            services.AddScoped<IReportCourseService, ReportCourseService>();
            services.AddScoped<ITaskStudentService, TaskStudentService>();
            services.AddScoped<INstructorService, InstructorService>();
            services.AddScoped<IModuleProgressService, ModuleProgressServices>();
            services.AddScoped<IPostService, PostService>();

            //Register AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //Register Authentication and authorization
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });
        }
    }
}


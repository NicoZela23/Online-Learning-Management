using AutoMapper;
using System.Reflection;
using Online_Learning_Management.Infrastructure.Repositories.Modules;
using Online_Learning_Management.Domain.Interfaces.Modules;
using Online_Learning_Management.Application.Modules.Services;
using Online_Learning_Management.Application.ModuleTasks.Services;
using Online_Learning_Management.Domain.Interfaces.ModuleTasks;
using Online_Learning_Management.Infrastructure.Repositories.ModuleTasks;
using Online_Learning_Management.Domain.Interfaces;
using Online_Learning_Management.Infrastructure.Repositories;
using Online_Learning_Management.Infrastructure.Repositories.Courses;
using Online_Learning_Management.Domain.Interfaces.GradeStudent;
using Online_Learning_Management.Application.GradeStudent.Services;
using Online_Learning_Management.Infrastructure.Repositories.GradeStudent;
using Online_Learning_Management.Domain.Interfaces.CourseStudents;
using Online_Learning_Management.Infrastructure.Repositories.CourseStudents;
using Online_Learning_Management.Domain.Interfaces.Forums;
using Online_Learning_Management.Infrastructure.Repositories.Forum;
using Online_Learning_Management.Application.Forums.Services;
using Online_Learning_Management.Domain.Interfaces.Files;
using Online_Learning_Management.Infrastructure.Repositories.Files;
using Online_Learning_Management.Application.Files.Services;

namespace Online_Learning_Management.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddProjectServices(this IServiceCollection services)
        {
          
            //Register repositories
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IModuleTaskRepository, ModuleTaskRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseStudentsRepository, CourseStudentsRepository>();
            services.AddScoped<IForumRepository, ForumRepository>();
            services.AddScoped<IGradeStudentRepository, GradeStudentRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
          
            //Register services
            services.AddScoped<IModuleService, ModuleServices>();
            services.AddScoped<IModuleTaskService, ModuleTaskService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICourseStudentsService, CourseStudentsService>();
            services.AddScoped<IForumService, ForumService>();
            services.AddScoped<IGradeStudentService, GradeStudentService>();
            services.AddScoped<IFileService, AzureBlobService>();


            //Register AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}


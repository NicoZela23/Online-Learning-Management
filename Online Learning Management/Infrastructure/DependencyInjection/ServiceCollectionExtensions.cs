using AutoMapper;
using System.Reflection;
using Online_Learning_Management.Infrastructure.Repositories.Modules;
using Online_Learning_Management.Domain.Interfaces.Modules;
using Online_Learning_Management.Application.Modules.Services;
using Online_Learning_Management.Domain.Interfaces.CourseStudents;
using Online_Learning_Management.Infrastructure.Repositories.CourseStudents;

namespace Online_Learning_Management.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddProjectServices(this IServiceCollection services)
        {
            //Register repositories
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<ICourseStudentsRepository, CourseStudentsRepository>();

            //Register services
            services.AddScoped<IModuleService, ModuleServices>();
            services.AddScoped<ICourseStudentsService, CourseStudentsService>();

            //Register AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}

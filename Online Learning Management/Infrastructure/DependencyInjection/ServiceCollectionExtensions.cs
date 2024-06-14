using Online_Learning_Management.Application.ModuleTasks.Services;
using Online_Learning_Management.Domain.Interfaces;
using Online_Learning_Management.Domain.Interfaces.ModuleTasks;
using Online_Learning_Management.Infrastructure.Repositories;
using Online_Learning_Management.Infrastructure.Repositories.ModuleTasks;
using System.Reflection;

namespace Online_Learning_Management.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddProjectServices(this IServiceCollection services)
        {
            //Register repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IModuleTaskRepository, ModuleTaskRepository>();

            //Register services
            services.AddScoped<IModuleTaskService, ModuleTaskService>();

            //Register AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}

using AutoMapper;
using System.Reflection;
using Online_Learning_Management.Domain.Interfaces;
using Online_Learning_Management.Infrastructure.Repositories.Modules;
using Online_Learning_Management.Infrastructure.Repositories;
using Online_Learning_Management.Domain.Interfaces.Modules;
using Online_Learning_Management.Application.Modules.Services;

namespace Online_Learning_Management.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddProjectServices(this IServiceCollection services)
        {
            //Register repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();

            //Register services
            services.AddScoped<IModuleService, ModuleServices>();

            //Register AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}

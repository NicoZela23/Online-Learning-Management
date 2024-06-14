using Online_Learning_Management.Domain.Entities.Modules;
using Online_Learning_Management.Infrastructure.DTOs.Module;

namespace Online_Learning_Management.Domain.Interfaces.Modules
{
    public interface IModuleService
    {
        Task<IEnumerable<Module>> GetAllModulesAsync();
        Task<Module> GetModuleByIdAsync(Guid id);
        Task AddModuleAsync(CreateModuleDTO module);
        Task UpdateModuleAsync(Guid id, UpdateModuleDTO module);
        Task DeleteModuleAsync(Guid id);
    }
}

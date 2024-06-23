using Online_Learning_Management.Domain.Entities.Modules;
using Online_Learning_Management.Infrastructure.DTOs.Module;

namespace Online_Learning_Management.Domain.Interfaces.Modules
{
    public interface IModuleService
    {
        Task<IEnumerable<Module>> GetAllModulesAsync();
        Task<Module> GetModuleByIdAsync(Guid id);
        Task<Module>AddModuleAsync(CreateModuleDTO module);
        Task<Module>UpdateModuleAsync(Guid id, UpdateModuleDTO module);
        Task DeleteModuleAsync(Guid id);
    }
}

using Online_Learning_Management.Domain.Entities.Modules;

namespace Online_Learning_Management.Domain.Interfaces.Modules
{
    public interface IModuleRepository
    {
        Task<Module> GetModuleByIdAsync(Guid id);
        Task<IEnumerable<Module>> GetAllModulesAsync();
        Task<Module>AddModuleAsync(Module module);
        Task<Module>UpdateModuleAsync(Module module);
        Task DeleteModuleAsync(Guid id);
        Task<IEnumerable<Module>> GetAllModulesAsync(string search);
    }
}

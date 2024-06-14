using Online_Learning_Management.Domain.Entities.Modules;

namespace Online_Learning_Management.Domain.Interfaces.Modules
{
    public interface IModuleRepository
    {
        Task<Module> GetModuleByIdAsync(Guid id);
        Task<IEnumerable<Module>> GetAllModulesAsync();
        Task AddModuleAsync(Module module);
        Task UpdateModuleAsync(Module module);
        Task DeleteModuleAsync(Guid id);
    }
}

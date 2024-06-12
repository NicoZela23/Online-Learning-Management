using Online_Learning_Management.Domain.Entities;

namespace Online_Learning_Management.Domain.Interfaces
{
    public interface IModuleRepository
    {
        Task<Module> GetModuleByIdAsync(Guid id);
        Task<Module> GetModuleByNameAsync(string name);
        Task<IEnumerable<Module>> GetAllModulesAsync();
        Task AddModuleAsync(Module module);

        Task UpdateModuleAsync(Module module);
        Task DeleteModuleAsync(Guid id);
    }
}

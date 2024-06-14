using Online_Learning_Management.Domain.Entities.ModuleTasks;

namespace Online_Learning_Management.Domain.Interfaces.ModuleTasks
{
    public interface IModuleTaskRepository
    {
        Task<IEnumerable<ModuleTask>> GetAllTasksOfModuleAsync();
        Task<ModuleTask> GetTaskOfModuleByIdAsync(Guid id);
        Task AddTaskToModuleAsync(ModuleTask moduleTask);
        Task UpdateTaskOfModuleAsync(ModuleTask moduleTask);
        Task DeleteTaskOfModuleAsync(Guid id);
    }
}

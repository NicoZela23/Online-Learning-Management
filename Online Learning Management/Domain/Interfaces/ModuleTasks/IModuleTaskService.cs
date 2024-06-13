using Online_Learning_Management.Domain.Entities.ModuleTasks;
using Online_Learning_Management.Infrastructure.DTOs.ModuleTask;

namespace Online_Learning_Management.Domain.Interfaces.ModuleTasks
{
    public interface IModuleTaskService
    {
        Task<IEnumerable<ModuleTask>> GetAllTasksOfModuleAsync();
        Task<ModuleTask> GetTaskOfModuloByIdAsync(Guid id);
        Task AddTaskToModuloAsync(CreateModuleTaskDTO moduleTask);
        Task UpdateTaskOfModuleAsync(Guid id, UpdateModuleTaskDTO moduleTask);
        Task DeleteTaskOfModuleAsync(Guid id);
    }
}

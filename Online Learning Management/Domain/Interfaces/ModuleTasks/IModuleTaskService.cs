using Online_Learning_Management.Application.ModuleTasks.Responses;
using Online_Learning_Management.Domain.Entities.ModuleTasks;
using Online_Learning_Management.Infrastructure.DTOs.ModuleTask;

namespace Online_Learning_Management.Domain.Interfaces.ModuleTasks
{
    public interface IModuleTaskService
    {
        Task<IEnumerable<ModuleTask>> GetAllTasksOfModuleAsync(Guid moduleID);
        Task<ModuleTask> GetTaskOfModuleByIdAsync(Guid id);
        Task<ModuleTask> AddTaskToModuleAsync(CreateModuleTaskDTO moduleTask);
        Task<ModuleTask> UpdateTaskOfModuleAsync(Guid id, UpdateModuleTaskDTO moduleTask);
        Task DeleteTaskOfModuleAsync(Guid id);
    }
}

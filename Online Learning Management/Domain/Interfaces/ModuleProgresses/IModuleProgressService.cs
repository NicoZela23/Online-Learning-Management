using Online_Learning_Management.Domain.Entities.ModuleProgresses;
using Online_Learning_Management.Infrastructure.DTOs.ModuleProgresses;

namespace OnlineLearningManagement.Domain.Interfaces
{
    public interface IModuleProgressService
    {
        Task<IEnumerable<ModuleProgress>> GetAllModuleProgressesAsync();
        Task<ModuleProgress> GetModuleProgressByIdAsync(Guid id);
        Task<ModuleProgress> AddModuleProgressAsync(CreateModuleProgressDTO moduleProgresses);
        Task<ModuleProgress> UpdateModuleProgressAsync(Guid id, UpdateModuleProgressDTO moduleProgresses);
        Task<ModuleProgress> PatchModuleProgressAsync(Guid id, PatchModuleProgressDTO progress);
        Task DeleteModuleProgressAsync(Guid id);
        Task<bool> CourseExists(Guid courseId);
        Task<bool> ModuleExists(Guid moduleId);
        Task<bool> StudentExists(Guid studentId);
    }
}
using Online_Learning_Management.Domain.Entities.ModuleProgresses;

namespace OnlineLearningManagement.Domain.Interfaces
{
    public interface IModuleProgressRepository
    {
        Task<IEnumerable<ModuleProgress>> GetAllModuleProgressesAsync();
        Task<ModuleProgress> GetModuleProgressByIdAsync(Guid id);
        Task<ModuleProgress> AddModuleProgressAsync(ModuleProgress ModuleProgresses);
        Task<ModuleProgress> UpdateModuleProgressAsync(ModuleProgress ModuleProgresses);
        Task<ModuleProgress> PatchModuleProgressAsync(Guid id, String progress);
        Task DeleteModuleProgressAsync(Guid id);
        Task<bool> ModuleProgressExistsAsync(Guid courseId, Guid moduleId, Guid studentId);
        Task<bool> CourseExistsAsync(Guid courseId);
        Task<bool> ModuleExistsAsync(Guid moduleId);
        Task<bool> StudentExistsAsync(Guid studentId);
    }
}
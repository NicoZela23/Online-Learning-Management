using Online_Learning_Management.Domain.Entities.ModuleProgresses;
using OnlineLearningManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Infrastructure.Data;

namespace Online_Learning_Management.Infrastructure.Repositories
{
    public class ModuleProgressesRepository : IModuleProgressRepository
    {
        private readonly ApplicationDbContext _context;

        public ModuleProgressesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ModuleProgress>> GetAllModuleProgressesAsync()
        {
            return await _context.ModuleProgresses.ToListAsync();
        }

        public async Task<ModuleProgress> GetModuleProgressByIdAsync(Guid id)
        {
            return await _context.ModuleProgresses.FirstOrDefaultAsync(mp => mp.Id == id);
        }

        public async Task<ModuleProgress> AddModuleProgressAsync(ModuleProgress moduleProgresses)
        {
            _context.ModuleProgresses.Add(moduleProgresses);
            await _context.SaveChangesAsync();
            return moduleProgresses;
        }

        public async Task<ModuleProgress> UpdateModuleProgressAsync(ModuleProgress moduleProgress)
        {
            _context.ModuleProgresses.Update(moduleProgress);
            await _context.SaveChangesAsync();
            return moduleProgress;
        }

        public async Task<ModuleProgress> PatchModuleProgressAsync(Guid id, String progress)
        {
            var moduleProgress = await _context.ModuleProgresses.FirstOrDefaultAsync(mp => mp.Id == id);
            moduleProgress.Progress = progress;
            await _context.SaveChangesAsync();
            return moduleProgress;
        }

        public async Task DeleteModuleProgressAsync(Guid id)
        {
            var moduleProgresses = await _context.ModuleProgresses.FirstOrDefaultAsync(mp => mp.Id == id);
            if (moduleProgresses != null)
            {
                _context.ModuleProgresses.Remove(moduleProgresses);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ModuleProgressExistsAsync(Guid courseId, Guid moduleId, Guid studentId)
        {
            return await _context.ModuleProgresses.AnyAsync(mp => mp.CourseId == courseId && mp.ModuleId == moduleId && mp.StudentId == studentId);
        }

        public async Task<bool> CourseExistsAsync(Guid courseId)
        {
            return await _context.Courses.AnyAsync(c => c.Id == courseId);
        }

        public async Task<bool> ModuleExistsAsync(Guid moduleId)
        {
            return await _context.Modules.AnyAsync(m => m.Id == moduleId);
        }

        public async Task<bool> StudentExistsAsync(Guid studentId)
        {
            return await _context.Students.AnyAsync(s => s.Id == studentId);
        }


    }
}
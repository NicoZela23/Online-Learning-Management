﻿using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities.ModuleTasks;
using Online_Learning_Management.Domain.Interfaces.ModuleTasks;
using Online_Learning_Management.Infrastructure.Data;

namespace Online_Learning_Management.Infrastructure.Repositories.ModuleTasks
{
    public class ModuleTaskRepository : IModuleTaskRepository
    {
        private readonly ApplicationDbContext _context;

        public ModuleTaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ModuleTask> AddTaskToModuleAsync(ModuleTask moduleTask)
        {
            var result = await _context.ModuleTasks.AddAsync(moduleTask);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteTaskOfModuleAsync(Guid id)
        {
            var moduleTask = await _context.ModuleTasks.FindAsync(id);
            if (moduleTask != null)
            {
                _context.ModuleTasks.Remove(moduleTask);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ModuleTask>> GetAllTasksOfModuleAsync(Guid moduleID)
        {
            return await _context.ModuleTasks.Where(t => t.ModuleID == moduleID).ToListAsync();
        }

        public async Task<ModuleTask> GetTaskOfModuleByIdAsync(Guid id)
        {
            return await _context.ModuleTasks.FindAsync(id);
        }

        public async Task UpdateTaskOfModuleAsync(ModuleTask moduleTask)
        {
            _context.ModuleTasks.Update(moduleTask);
            await _context.SaveChangesAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities.Modules;
using Online_Learning_Management.Domain.Interfaces.Modules;
using Online_Learning_Management.Infrastructure.Data;

namespace Online_Learning_Management.Infrastructure.Repositories.Modules
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly ApplicationDbContext _context;

        public ModuleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Module> GetModuleByIdAsync(Guid id)
        {
            return await _context.Modules.FindAsync(id);
        }

        public async Task<IEnumerable<Module>> GetAllModulesAsync()
        {
            return await _context.Modules.ToListAsync();
        }
        public async Task<Module> AddModuleAsync(Module module)
        {
            var result = await _context.Modules.AddAsync(module);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task <Module> UpdateModuleAsync(Module module)
        {
            var result = _context.Modules.Update(module);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteModuleAsync(Guid id)
        {
            var module = await _context.Modules.FindAsync(id);
            if (module != null)
            {
                _context.Modules.Remove(module);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Module>> GetAllModulesAsync(string search)
        {
            return await _context.Modules
                         .Where(m => m.Name.Contains(search) || m.Description.Contains(search))
                         .ToListAsync();
        }
    }
}

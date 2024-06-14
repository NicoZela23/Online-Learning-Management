using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities;
using Online_Learning_Management.Domain.Entities.Modules;
using Online_Learning_Management.Infrastructure.Repositories.Modules;
using Online_Learning_Management.Domain.Entities.ModuleTasks;
using Online_Learning_Management.Infrastructure.Repositories.ModuleTasks;

namespace Online_Learning_Management.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Module> Modules{ get; set; }
        public DbSet<ModuleTask> ModuleTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ModuleConfiguration());
            modelBuilder.ApplyConfiguration(new ModuleTaskConfiguration());
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities;

namespace Online_Learning_Management.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}

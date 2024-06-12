using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Learning_Management.Domain.Entities.Module;

namespace Online_Learning_Management.Infrastructure.Repositories.Modules
{
    public class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CourseID).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x=>x.Description).IsRequired();
        }
    }
}

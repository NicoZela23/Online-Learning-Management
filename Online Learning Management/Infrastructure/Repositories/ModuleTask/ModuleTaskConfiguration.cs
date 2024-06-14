using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Learning_Management.Domain.Entities.ModuleTasks;

namespace Online_Learning_Management.Infrastructure.Repositories.ModuleTasks
{
    public class ModuleTaskConfiguration : IEntityTypeConfiguration<ModuleTask>
    {
        public void Configure(EntityTypeBuilder<ModuleTask> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ModuleID)
                .IsRequired();
            builder.Property(x => x.Title)
               .IsRequired()
               .HasColumnType("nvarchar(100)");

            builder.Property(x => x.Description)
                .IsRequired()
                .HasColumnType("text");
        }
    }
}

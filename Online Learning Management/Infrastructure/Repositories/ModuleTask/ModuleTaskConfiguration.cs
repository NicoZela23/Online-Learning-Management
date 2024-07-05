using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Learning_Management.Domain.Entities.Modules;
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

            builder.Property(x => x.Type)
               .IsRequired()
               .HasColumnType("nvarchar(50)");

            builder.Property(x => x.DateCreated)
                .HasColumnType("date")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.Deadline)
                .HasColumnType("datetime");

            builder.HasOne<Module>()
                  .WithMany()
                  .HasForeignKey(m => m.ModuleID)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

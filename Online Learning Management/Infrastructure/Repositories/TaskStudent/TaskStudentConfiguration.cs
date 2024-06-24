using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Learning_Management.Domain.Entities.Files;
using Online_Learning_Management.Domain.Entities.ModuleTasks;
using Online_Learning_Management.Domain.Entities.Students;
using Online_Learning_Management.Domain.Entities.TaskStudents;

namespace Online_Learning_Management.Infrastructure.Repositories.TaskStudents
{
    public class TaskStudentConfiguration : IEntityTypeConfiguration<TaskStudent>
    {
        public void Configure(EntityTypeBuilder<TaskStudent> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ModuleTaskID)
                .IsRequired();

            builder.Property(x => x.StudentID)
                .IsRequired();

            builder.Property(x => x.FileID)
                .IsRequired();

            builder.Property(x => x.Qualification)
                .HasColumnType("int")
                .HasDefaultValue(0)
                .IsRequired(false);

            builder.Property(x => x.Comment)
                .HasColumnType("nvarchar(250)")
                .IsRequired(false);

            builder.Property(x => x.UploadDate)
                .IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne<ModuleTask>()
                  .WithMany()
                  .HasForeignKey(m => m.ModuleTaskID)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Student>()
                  .WithMany()
                  .HasForeignKey(m => m.StudentID)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<FileMetadata>()
                  .WithMany()
                  .HasForeignKey(m => m.FileID)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Learning_Management.Domain.Entities.ModuleProgresses;
using Online_Learning_Management.Domain.Entities.Courses;
using Online_Learning_Management.Domain.Entities.Students;
using Online_Learning_Management.Domain.Entities.Modules;

namespace Online_Learning_Management.Infrastructure.Repositories
{
    public class ModuleProgressesConfiguration : IEntityTypeConfiguration<ModuleProgress>
    {
        public void Configure(EntityTypeBuilder<ModuleProgress> builder)
        {
            builder.HasKey(mp => mp.Id);

            builder.Property(mp => mp.CourseId).IsRequired();
            builder.Property(mp => mp.ModuleId).IsRequired();
            builder.Property(mp => mp.StudentId).IsRequired();
            builder.Property(mp => mp.Progress).IsRequired().HasConversion<string>();

            builder.HasOne<Course>()
                   .WithMany()
                   .HasForeignKey(mp => mp.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne<Student>()
                     .WithMany()
                     .HasForeignKey(mp => mp.StudentId)
                     .OnDelete(DeleteBehavior.Cascade); ;

            builder.HasOne<Module>()
                        .WithMany()
                        .HasForeignKey(mp => mp.ModuleId)
                        .OnDelete(DeleteBehavior.Cascade); ;
        }
    }
}
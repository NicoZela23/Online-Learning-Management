using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Learning_Management.Domain.Entities.Courses;
using Online_Learning_Management.Domain.Entities.Instructors;

namespace Online_Learning_Management.Infrastructure.Repositories.Courses
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Title).IsRequired();
            builder.Property(c => c.Description).IsRequired();
            builder.Property(c => c.IdInstructor).IsRequired();
            builder.Property(c => c.Content).IsRequired();
            builder.Property(c => c.DurationInWeeks).IsRequired();

            builder.HasOne<Instructor>()
                   .WithMany()
                   .HasForeignKey(c => c.IdInstructor)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

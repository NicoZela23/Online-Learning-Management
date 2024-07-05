using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Learning_Management.Domain.Entities.Courses;
using Online_Learning_Management.Domain.Entities.CourseStudent;
using Online_Learning_Management.Domain.Entities.Students;
namespace Online_Learning_Management.Infrastructure.Repositories
{
    public class CourseStudentConfiguration : IEntityTypeConfiguration<CourseStudent>
    {
        public void Configure(EntityTypeBuilder<CourseStudent> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CourseID).IsRequired();
            builder.Property(x => x.StudentID).IsRequired();
            builder.Property(x => x.EnrollmentDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");
            builder.HasOne<Course>().WithMany()
                .HasForeignKey(x => x.CourseID)
                .OnDelete(DeleteBehavior.Cascade); ;
            builder.HasOne<Student>().WithMany()
                .HasForeignKey(x => x.StudentID)
                .OnDelete(DeleteBehavior.Cascade); ;
        }
    }
}
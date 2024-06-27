using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Learning_Management.Domain.Entities.ReportCourses;

namespace Online_Learning_Management.Infrastructure.Repositories.ReportCourses
{
    public class ReportCourseConfiguration : IEntityTypeConfiguration<ReportCourse>
    {
        public void Configure(EntityTypeBuilder<ReportCourse> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.StudentID).IsRequired();
            builder.Property(x => x.CourseID).IsRequired();
            builder.Property(x => x.StudentName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.CourseName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ProgressPercentage).IsRequired().HasDefaultValue(0.0);

            builder.HasIndex(x => new { x.StudentID, x.CourseID }).IsUnique();
        }
    }
}

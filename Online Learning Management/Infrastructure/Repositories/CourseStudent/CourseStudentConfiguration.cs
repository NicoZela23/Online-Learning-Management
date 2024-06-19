using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Learning_Management.Domain.Entities.CourseStudent;
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
            builder.Property(x => x.Progress).IsRequired();

        }
    }
}
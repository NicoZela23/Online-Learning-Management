using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Learning_Management.Domain.Entities.Courses;
using Online_Learning_Management.Domain.Entities.GradeStudents;
using Online_Learning_Management.Domain.Entities.Students;
using Online_Learning_Management.Domain.Interfaces;
using Online_Learning_Management.Infrastructure.Data;

namespace Online_Learning_Management.Infrastructure.Repositories.GradeStudentss
{
    public class GradeStudentConfiguration : IEntityTypeConfiguration<GradeStudents>
    {
        public void Configure(EntityTypeBuilder<GradeStudents> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.StudentId)
                .IsRequired();
            builder.Property(x => x.CourseId)
                .IsRequired();
            builder.Property(x => x.Score)
                .IsRequired()
                .HasColumnType("decimal(5, 2)")
                .HasPrecision(5, 2)
                ;

            builder.ToTable("Grades", t =>
            {
                t.HasCheckConstraint("CK_Grade_Score", "[Score] >= 0 AND [Score] <= 100");
            });
            builder.HasOne<Student>()
                .WithMany()
                .HasForeignKey(x => x.StudentId);
            builder.HasOne<Course>().WithMany().HasForeignKey(x => x.CourseId);
        }
    }
}

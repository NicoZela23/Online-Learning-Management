using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Learning_Management.Domain.Entities.Courses;
using Online_Learning_Management.Domain.Entities.Modules;
using System.Text.Json;

namespace Online_Learning_Management.Infrastructure.Repositories.Modules
{
    public class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CourseID)
                   .IsRequired();

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasColumnType("nvarchar(100)");

            builder.Property(x => x.Description)
                   .IsRequired()
                   .HasColumnType("nvarchar(3000)");

            builder.Property(x => x.Duration)
                   .IsRequired();

            builder.Property(x => x.LearningOutcomes)
                   .HasConversion(
                       v => string.Join(',', v),
                       v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

            builder.Property(x => x.Prerequisites)
                   .HasConversion(
                       v => string.Join(',', v),
                       v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

            builder.Property(x => x.StartDate)
                    .IsRequired();

            builder.Property(x => x.EndDate)
                   .IsRequired();
                   
            builder.Property(x => x.Resources)
                   .HasConversion(
                       v => string.Join(',', v),
                       v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
                   .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            builder.Property(x => x.AssessmentMethods)
                   .HasConversion(
                       v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                       v => JsonSerializer.Deserialize<Dictionary<string, int>>(v, (JsonSerializerOptions)null));

            builder.HasOne<Course>()
                   .WithMany()
                   .HasForeignKey(m => m.CourseID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
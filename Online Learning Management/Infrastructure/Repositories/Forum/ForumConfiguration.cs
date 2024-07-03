using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Online_Learning_Management.Domain.Entities.Forums.Forum;
using ForumEntity = Online_Learning_Management.Domain.Entities.Forums.Forum; // Alias para evitar conflictos

namespace Online_Learning_Management.Infrastructure.Repositories.Forum
{
    public class ForumConfiguration : IEntityTypeConfiguration<ForumEntity>
    {
        public void Configure(EntityTypeBuilder<ForumEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CourseID).IsRequired();
            builder.Property(x => x.Title)
               .IsRequired()
               .HasColumnType("nvarchar(100)");

            builder.Property(x => x.Description)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.HasOne(f => f.Course)
                .WithMany(c => c.Forums)
                .HasForeignKey(f => f.CourseID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

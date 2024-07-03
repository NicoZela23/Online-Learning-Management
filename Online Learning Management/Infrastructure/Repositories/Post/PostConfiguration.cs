
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Learning_Management.Domain.Entities.Post;
using Online_Learning_Management.Domain.Entities.Forums;

namespace Online_Learning_Management.Infrastructure.Repositories.Post.PostConfiguration
{
    public class PostConfiguration : IEntityTypeConfiguration<Posts>
    {
        public void Configure(EntityTypeBuilder<Posts> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Content)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(x => x.CreatedAt)
                
                .HasDefaultValueSql("GETDATE()"); 

            builder.Property(x => x.UpdatedAt)
                .IsRequired(false);
            

            builder.Property(x => x.ForumId)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            // // Define relationships
            builder.HasOne(p => p.Forum)
                 .WithMany(f => f.Posts)
                 .HasForeignKey(p => p.ForumId)
                 .OnDelete(DeleteBehavior.Cascade);

            
        }
    }
}


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Learning_Management.Domain.Entities.Files;

namespace Online_Learning_Management.Infrastructure.Repositories.Files
{
    public class FileConfiguration : IEntityTypeConfiguration<FileMetadata>
    {
        public void Configure(EntityTypeBuilder<FileMetadata> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FileName)
                .IsRequired()
                .HasColumnType("nvarchar(40)");

            builder.Property(x => x.BlobURL)
                .IsRequired()
                .HasColumnType("nvarchar(200)");

            builder.Property(x => x.FileSize)
                .IsRequired();

            builder.Property(x => x.UploadedAt)
                .IsRequired();
        }
    }
}

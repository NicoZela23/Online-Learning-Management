using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Learning_Management.Domain.Entities.Auth;
using Online_Learning_Management.Domain.Entities.Students;

namespace Online_Learning_Management.Infrastructure.Repositories.Students
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserId);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(50)
                .HasAnnotation("Relational:ColumnType", "varchar(50)");

            builder.Property(x => x.CreateAt)
                .IsRequired();

            builder.HasIndex(x => x.Email).IsUnique();

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

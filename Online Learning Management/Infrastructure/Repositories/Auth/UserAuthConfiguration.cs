using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Learning_Management.Domain.Entities.Auth;

namespace Online_Learning_Management.Infrastructure.Repositories.Auth
{
    public class UserAuthConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Username).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.EmailAddress).IsRequired();
            builder.Property(x => x.Role).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Learning_Management.Domain.Entities;

namespace Online_Learning_Management.Infrastructure.Repositories.Student
{
    public class StudentsConfiguration : IEntityTypeConfiguration<Students>
    {
        public void Configure(EntityTypeBuilder<Students> builder)
        {
            builder.HasKey(s => s.Id);
            // Aquí puedes agregar más configuraciones de acuerdo a tus necesidades
        }
    }
}

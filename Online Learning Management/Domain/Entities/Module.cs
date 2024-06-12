using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Management.Domain.Entities
{
    public class Module
    {
        [Key]
        public Guid Id { get; set; }
        public required string CourseID { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}

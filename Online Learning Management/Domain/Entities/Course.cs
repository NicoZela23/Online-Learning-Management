using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Management.Domain.Entities
{
    public class Course
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required int IdInstructor { get; set; }

    }
}

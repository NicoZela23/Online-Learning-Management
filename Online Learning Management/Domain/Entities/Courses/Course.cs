using Online_Learning_Management.Domain.Entities.Forums;
using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Management.Domain.Entities.Courses
{
    public class Course
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required Guid IdInstructor { get; set; }
        public List<string> Content { get; set; }
        public int DurationInWeeks { get; set; }  
        public ICollection<Forum> Forums { get; set; } = new List<Forum>();
    }
}

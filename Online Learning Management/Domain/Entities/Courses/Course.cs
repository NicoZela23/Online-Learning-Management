using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Management.Domain.Entities.Courses
{
    public class Course
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public Guid IdInstructor { get; set; }
        public List<string> Content { get; set; }
        public int DurationInWeeks { get; set; }  
    }
}

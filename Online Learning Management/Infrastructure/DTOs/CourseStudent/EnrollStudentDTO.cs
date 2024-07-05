using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Management.Infrastructure.DTOs.CourseStudents

{
    public class EnrollStudentDTO
    {
        [Required]
        public Guid StudentID { get; set; }
        [Required]
        public Guid CourseID { get; set; }
    }
}
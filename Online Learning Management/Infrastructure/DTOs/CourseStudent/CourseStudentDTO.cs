using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Management.Infrastructure.DTOs.CourseStudents
{
    public class CourseStudentDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string CourseID { get; set; }

        [Required]
        public string StudentID { get; set; }
    }
}
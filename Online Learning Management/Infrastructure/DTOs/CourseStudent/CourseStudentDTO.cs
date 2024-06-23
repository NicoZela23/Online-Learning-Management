using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Management.Infrastructure.DTOs.CourseStudents
{
    public class CourseStudentDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid CourseID { get; set; }

        [Required]
        public Guid StudentID { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public decimal Progress { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Management.Infrastructure.DTOs.ReportCourse
{
    public class ReportCourseDTO
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid StudentID { get; set; }
        [Required]
        public Guid CourseID { get; set; }
        public string StudentName { get; set; }
        public string CourseName { get; set; }
        public double ProgressPercentage { get; set; }
    }
}

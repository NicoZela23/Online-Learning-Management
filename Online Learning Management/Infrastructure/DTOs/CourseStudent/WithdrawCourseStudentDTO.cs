using System.ComponentModel.DataAnnotations;


namespace Online_Learning_Management.Infrastructure.DTOs.CourseStudents
{

public class WithdrawCourseStudentRequest
{
    [Required]
    public Guid StudentId { get; set; }
    [Required]
    public Guid CourseId { get; set; }
}
}   
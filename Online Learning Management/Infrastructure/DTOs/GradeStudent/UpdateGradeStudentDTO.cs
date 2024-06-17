using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Management.Infrastructure.DTOs.GradeStudent
{
    public class UpdateGradeStudentDTO
    {
        [Required]
        public decimal? Score { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}

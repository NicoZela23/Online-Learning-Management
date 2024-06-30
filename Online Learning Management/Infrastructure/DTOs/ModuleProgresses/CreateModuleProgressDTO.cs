using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Management.Infrastructure.DTOs.ModuleProgresses
{
    public class CreateModuleProgressDTO
    {
        [Required]
        public Guid CourseId { get; set; }
        [Required]
        public Guid ModuleId { get; set; }
        [Required]
        public Guid StudentId { get; set; }
        [Required]
        public String Progress { get; set; }
    }
}
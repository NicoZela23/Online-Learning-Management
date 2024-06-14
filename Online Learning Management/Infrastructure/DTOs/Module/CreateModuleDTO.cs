using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Management.Infrastructure.DTOs.Module
{
    public class CreateModuleDTO
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? CourseID { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}

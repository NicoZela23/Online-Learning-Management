using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Management.Infrastructure.DTOs.ModuleTask
{
    public class CreateModuleTaskDTO
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? ModuleID { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}

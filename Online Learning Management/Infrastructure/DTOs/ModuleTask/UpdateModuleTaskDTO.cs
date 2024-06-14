using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Management.Infrastructure.DTOs.ModuleTask
{
    public class UpdateModuleTaskDTO
    {
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}

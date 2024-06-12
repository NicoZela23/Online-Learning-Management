using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Management.Application.DTOs.Module
{
    public class UpdateModuleDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}

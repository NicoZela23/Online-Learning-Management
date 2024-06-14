using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Management.Infrastructure.DTOs.Forum
{
    public class UpdateForumDTO
    {
        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Management.Infrastructure.DTOs.Forum
{
    public class UpdateForumDTO
    {
        
        public string? Title { get; set; }
        
        public string? Description { get; set; }
    }
}

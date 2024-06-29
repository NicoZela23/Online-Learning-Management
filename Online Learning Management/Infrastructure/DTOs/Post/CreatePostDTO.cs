using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Management.Infrastructure.DTOs.Post
{
    public class CreatePostDTO
    {
        [Key]
        public Guid ForumId { get; set; }
        [Required]
        public string? Content { get; set; }
    }
}

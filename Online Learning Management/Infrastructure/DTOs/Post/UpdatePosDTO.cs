using System.ComponentModel.DataAnnotations;


namespace Online_Learning_Management.Infrastructure.DTOs.Post
{
    public class UpdatePostDTO
    {
        [Required]
        public string? Content { get; set; }
    }
}

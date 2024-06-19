using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Management.Infrastructure.DTOs.Forum
{
    public class CreateForumDTO
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? CourseID { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}

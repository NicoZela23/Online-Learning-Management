using Online_Learning_Management.Domain.Entities.Forums;

namespace Online_Learning_Management.Domain.Entities.Post
{
    public class Posts
    {
        public Guid Id { get; set; }
        public Guid ForumId { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation property
        public Forum? Forum { get; set; }
    }
}

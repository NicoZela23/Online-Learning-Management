using Online_Learning_Management.Domain.Entities.Courses;
using Online_Learning_Management.Domain.Entities.Post;

namespace Online_Learning_Management.Domain.Entities.Forums
{
    public class Forum
    {
        public Guid Id { get; set; }
        public Guid CourseID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        

        //Navigation Property
        //public Course? Course { get; set; }
        //public ICollection<Posts>? Posts { get; set; } = new List<Posts>();
    }
}

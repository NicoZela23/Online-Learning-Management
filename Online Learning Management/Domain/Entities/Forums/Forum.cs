namespace Online_Learning_Management.Domain.Entities.Forums
{
    public class Forum
    {
        public Guid Id { get; set; }
        public string? CourseID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}

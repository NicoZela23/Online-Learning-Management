namespace Online_Learning_Management.Domain.Entities.Modules
{
    public class Module
    {
        public Guid Id { get; set; }
        public string? CourseID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}

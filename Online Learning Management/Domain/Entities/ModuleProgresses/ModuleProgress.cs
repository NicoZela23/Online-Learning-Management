

namespace Online_Learning_Management.Domain.Entities.ModuleProgresses
{
    public class ModuleProgress
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public Guid ModuleId { get; set; }
        public Guid StudentId { get; set; }
        public String Progress { get; set; }
    }
}

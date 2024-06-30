namespace Online_Learning_Management.Infrastructure.DTOs.ModuleProgresses
{
    public class UpdateModuleProgressDTO
    {
        public Guid CourseId { get; set; }
        public Guid ModuleId { get; set; }
        public Guid StudentId { get; set; }
        public String Progress { get; set; }
    }
}
using Online_Learning_Management.Domain.Entities.Files;
using Online_Learning_Management.Domain.Entities.ModuleTasks;
using Online_Learning_Management.Domain.Entities.Students;

namespace Online_Learning_Management.Domain.Entities.TaskStudents
{
    public class TaskStudent
    {
        public Guid Id { get; set; }
        public Guid ModuleTaskID { get; set; }
        public Guid StudentID { get; set; }
        public Guid FileID { get; set; }
        public int? Qualification { get; set; }
        public string? Comment { get; set; }
        public DateTime? UploadDate { get; set; }
    }
}

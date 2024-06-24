namespace Online_Learning_Management.Infrastructure.DTOs.TaskStudent
{
    public class CreateTaskStudentDTO
    {
        public Guid? ModuleTaskID { get; set; }
        public Guid? StudentID { get; set; }
        public Guid? FileID { get; set; }
    }
}

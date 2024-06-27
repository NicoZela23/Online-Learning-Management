using Online_Learning_Management.Domain.Entities.TaskStudents;

namespace Online_Learning_Management.Domain.Interfaces.TaskStudents
{
    public interface ITaskStudentRepository
    {
        Task<TaskStudent> UploadTaskAsync(TaskStudent taskStudent);
    }
}

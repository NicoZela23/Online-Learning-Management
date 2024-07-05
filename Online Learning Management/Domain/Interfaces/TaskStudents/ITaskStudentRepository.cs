using Online_Learning_Management.Domain.Entities.TaskStudents;
using Online_Learning_Management.Infrastructure.DTOs.TaskStudent;

namespace Online_Learning_Management.Domain.Interfaces.TaskStudents
{
    public interface ITaskStudentRepository
    {
        Task<TaskStudent> UploadTaskAsync(TaskStudent taskStudent);
        Task<IEnumerable<Object>> GetAllSubmittedTasksAsync(Guid taskID);
        Task<Object> GetRelationshipDataFromTaaskStudents(Guid taskID);
        Task<Object> GetSubmittedTaskByIdAsync(Guid studentTaskId);
        Task<TaskStudent> UpdateTaskStudentAsync(Guid studentTaskId, UpdateTaskStudentDTO taskStudentDto);
    }
}

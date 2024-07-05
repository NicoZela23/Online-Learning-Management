using Online_Learning_Management.Application.TaskStudent.Responses;
using Online_Learning_Management.Domain.Entities.TaskStudents;
using Online_Learning_Management.Infrastructure.DTOs.TaskStudent;

namespace Online_Learning_Management.Domain.Interfaces.TaskStudents
{
    public interface ITaskStudentService
    {
        Task<TaskStudent> UploadTaskAsync(Guid moduleTaskId, Guid studentId, Guid fileId);
        Task<Object> GetAllSubmittedTasksAsync(Guid taskID);
        Task<RelationshipDataStructure> StructureResponse(
            TaskStudent taskStudent,
            Guid taskID,
            Guid studentID,
            Guid fileID
        );
        Task<Object> GetSubmittedTaskByIdAsync(Guid studentTaskId);
        Task<Object> UpdateTaskStudenAsync(Guid studentTaskId, UpdateTaskStudentDTO taskStudentDTO);
    }
}

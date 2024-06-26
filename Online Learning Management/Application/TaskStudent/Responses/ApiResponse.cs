using TaskStudentEntity = Online_Learning_Management.Domain.Entities.TaskStudents.TaskStudent;

namespace Online_Learning_Management.Application.TaskStudent.Responses
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public TaskStudentEntity Data { get; set; }
        public RelationshipDataStructure RelationshipData { get; set; }

        public ApiResponse(string message, TaskStudentEntity data, RelationshipDataStructure relationshipData)
        {
            Message = message;
            Data = data;
            RelationshipData = relationshipData;
        }
    }
}

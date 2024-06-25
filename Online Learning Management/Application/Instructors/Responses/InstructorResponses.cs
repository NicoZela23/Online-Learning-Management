using Online_Learning_Management.Domain.Entities.Instructors;

namespace Online_Learning_Management.Application.Instructors.Responses
{
    public class InstructorResponses
    {
        public string Message { get; set; }
        public Instructor Data { get; set; }

        public InstructorResponses(string message, Instructor data)
        {
            Message = message;
            Data = data;
        }
    }
}

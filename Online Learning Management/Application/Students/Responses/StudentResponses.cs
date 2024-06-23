using Online_Learning_Management.Domain.Entities.Students;

namespace Online_Learning_Management.Application.Students.Responses
{
    public class StudentResponses
    {
        public string Message { get; set; }
        public Student Data { get; set; }
        public StudentResponses(string message , Student data)
        {
            Message = message;
            Data = data;
        }
    }
}

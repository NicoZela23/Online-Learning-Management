using Online_Learning_Management.Domain.Entities.Files;
using Online_Learning_Management.Domain.Entities.ReportCourses;

namespace Online_Learning_Management.Application.ReportCourses.Responses
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public ReportCourse Data { get; set; }

        public ApiResponse(string message, ReportCourse data)
        {
            Message = message;
            Data = data;
        }
    }
}

using Online_Learning_Management.Domain.Entities.Auth;

namespace Online_Learning_Management.Application.Auth.Responses
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public User Data { get; set; }

        public ApiResponse(string message, User data)
        {
            Message = message;
            Data = data;
        }
    }
}

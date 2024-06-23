using Online_Learning_Management.Domain.Entities.Modules;

namespace Online_Learning_Management.Application.Modules.Responses
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public Module Data { get; set; }

        public ApiResponse(string message, Module data)
        {
            Message = message;
            Data = data;
        }
    }
}

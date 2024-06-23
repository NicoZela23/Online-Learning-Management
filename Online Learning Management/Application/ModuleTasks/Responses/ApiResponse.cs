using Online_Learning_Management.Domain.Entities.ModuleTasks;

namespace Online_Learning_Management.Application.ModuleTasks.Responses
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public ModuleTask Data { get; set; }

        public ApiResponse(string message, ModuleTask data)
        {
            Message = message;
            Data = data;
        }
    }
}

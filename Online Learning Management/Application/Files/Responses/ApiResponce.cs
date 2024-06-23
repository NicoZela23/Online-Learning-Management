using Online_Learning_Management.Domain.Entities.Files;

namespace Online_Learning_Management.Application.Files.Responses
{
    public class ApiResponce
    {
        public string Message { get; set; }
        public FileMetadata Data { get; set; }

        public ApiResponce(string message, FileMetadata data)
        {
            Message = message;
            Data = data;
        }
    }
}

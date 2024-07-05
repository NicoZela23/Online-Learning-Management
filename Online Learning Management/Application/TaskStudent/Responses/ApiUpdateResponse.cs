namespace Online_Learning_Management.Application.TaskStudent.Responses
{
    public class ApiUpdateResponse
    {
        public string Message { get; set; }
        public Object Data {  get; set; }

        public ApiUpdateResponse(string message, Object data)
        {
            Message = message;
            Data = data;
        }
    }
}

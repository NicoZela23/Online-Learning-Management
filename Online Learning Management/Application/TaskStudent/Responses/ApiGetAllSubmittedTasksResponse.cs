namespace Online_Learning_Management.Application.TaskStudent.Responses
{
    public class ApiGetAllSubmittedTasksResponse
    {
        public Object Information { get; set; }
        public IEnumerable<Object> SubmittedTasks {  get; set; }

        public ApiGetAllSubmittedTasksResponse(IEnumerable<Object> submittedTasks, Object information)
        {
            SubmittedTasks = submittedTasks;
            Information = information;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Online_Learning_Management.Application.Files.Responses;
using Online_Learning_Management.Application.TaskStudent.helpers;
using Online_Learning_Management.Application.TaskStudent.Responses;
using Online_Learning_Management.Domain.Interfaces.Files;
using Online_Learning_Management.Domain.Interfaces.TaskStudents;
using System.Threading.Tasks;

namespace Online_Learning_Management.Presentation.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskStudentsController : ControllerBase
    {
        private readonly ITaskStudentService _taskStudentService;
        private readonly IFileService _fileService;

        public TaskStudentsController(ITaskStudentService taskStudentService, IFileService fileService)
        {
            _taskStudentService = taskStudentService;
            _fileService = fileService;
        }

        [HttpPost("{taskID}/students/{studentID}")]
        public async Task<ActionResult> UploadTask(Guid taskID, Guid studentID, IFormFile file)
        {
            try
            {
                var validationError = VerifyPdfFile.ValidateFile(file);
                if (validationError != null)
                {
                    return BadRequest(validationError);
                }

                var fileMetadata = await _fileService.UploadAndAddFileAsync(file);
                var fileID = fileMetadata.Id;

                var taskUpload = await _taskStudentService.UploadTaskAsync(taskID, studentID, fileID);

                var structureResponse = await _taskStudentService.StructureResponse(
                    taskUpload, taskID, studentID, fileID
                );

                var response = new ApiResponse("Task uploaded successfully", taskUpload, structureResponse);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

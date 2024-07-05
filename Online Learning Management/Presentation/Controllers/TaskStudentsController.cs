using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Management.Application.Files.Responses;
using Online_Learning_Management.Application.TaskStudent.helpers;
using Online_Learning_Management.Application.TaskStudent.Responses;
using Online_Learning_Management.Domain.Entities.Students;
using Online_Learning_Management.Domain.Interfaces.Files;
using Online_Learning_Management.Domain.Interfaces.TaskStudents;
using Online_Learning_Management.Infrastructure.DTOs.TaskStudent;
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

        [HttpPost("{taskID}/students/{studentID}/submit")]
        [Authorize(Roles = "Student")]
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

        [HttpGet("{taskID}/submittedTasks")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Object>>> GetAllSubmittedTasks(Guid taskID)
        {
            try
            {
                var response = await _taskStudentService.GetAllSubmittedTasksAsync(taskID);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("submittedTasks/{studentTaskId}")]
        [Authorize]
        public async Task<ActionResult> GetSubmittedTasksById(Guid studentTaskId)
        {
            try
            {
                var response = await _taskStudentService.GetSubmittedTaskByIdAsync(studentTaskId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("submittedTasks/{studentTaskId}")]
        [Authorize]
        public async Task<ActionResult> UpdateSubmittedTask(
            Guid studentTaskId, 
            [FromBody] UpdateTaskStudentDTO taskStudentDto
        )
        {
            try
            {
                var response = await _taskStudentService.UpdateTaskStudenAsync(studentTaskId, taskStudentDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

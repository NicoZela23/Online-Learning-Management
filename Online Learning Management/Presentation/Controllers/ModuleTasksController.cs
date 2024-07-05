using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Management.Application.ModuleTasks.Responses;
using Online_Learning_Management.Domain.Entities.ModuleTasks;
using Online_Learning_Management.Domain.Interfaces.ModuleTasks;
using Online_Learning_Management.Infrastructure.DTOs.ModuleTask;
using System.Reflection;

namespace Online_Learning_Management.Presentation.Controllers
{
    [Route("api/modules")]
    [ApiController]
    public class ModuleTasksController : ControllerBase
    {
        private readonly IModuleTaskService _moduleTaskService;

        public ModuleTasksController(IModuleTaskService moduleTaskService)
        {
            _moduleTaskService = moduleTaskService;
        }

        [HttpGet("{moduleID}/tasks")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ModuleTask>>> GetAllTasksModule(Guid moduleID)
        {
            try
            {
                var moduleTask = await _moduleTaskService.GetAllTasksOfModuleAsync(moduleID);
                return Ok(moduleTask);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("tasks/{taskID}")]
        [Authorize]
        public async Task<ActionResult<ModuleTask>> GetTaskOfModuleById(Guid taskID)
        {
            try
            {
                var moduleTask = await _moduleTaskService.GetTaskOfModuleByIdAsync(taskID);

                if (moduleTask == null)
                {
                    return NotFound();
                }

                return Ok(moduleTask);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("tasks")]
        [Authorize(Roles = "Instructor")]
        public async Task<ActionResult> AddTaskToModule(CreateModuleTaskDTO moduleTaskDto)
        {
            try
            {
                var createdModuleTask = await _moduleTaskService.AddTaskToModuleAsync(moduleTaskDto);
                var response = new ApiResponse(
                    "Module task created successfully",
                    createdModuleTask
                );

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("tasks/{taskID}")]
        [Authorize(Roles = "Instructor")]
        public async Task<ActionResult> UpdateTaskOfModule(
            Guid taskID, 
            [FromBody] UpdateModuleTaskDTO moduleTaskDto)
        {
            try
            {
                var updatedModuleTask = await _moduleTaskService.UpdateTaskOfModuleAsync(taskID, moduleTaskDto);
                var response = new ApiResponse(
                    "Module task updated successfully",
                    updatedModuleTask
                );

                return Ok(response);
            }
            catch (ArgumentException)
            {
                return NotFound("The task does not exist.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("tasks/{taskID}")]
        [Authorize(Roles = "Instructor")]
        public async Task<ActionResult> DeleteTaskOfModule(Guid taskID)
        {
            try
            {
                await _moduleTaskService.DeleteTaskOfModuleAsync(taskID);
                return Ok(new { message = "Task deleted successfully" });
            }
            catch (ArgumentException)
            {
                return NotFound("The task does not exist.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

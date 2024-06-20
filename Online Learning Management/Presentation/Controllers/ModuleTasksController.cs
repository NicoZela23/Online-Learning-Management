using Microsoft.AspNetCore.Mvc;
using Online_Learning_Management.Domain.Entities.ModuleTasks;
using Online_Learning_Management.Domain.Interfaces.ModuleTasks;
using Online_Learning_Management.Infrastructure.DTOs.ModuleTask;
using System.Reflection;

namespace Online_Learning_Management.Presentation.Controllers
{
    [Route("api/modules/tasks")]
    [ApiController]
    public class ModuleTasksController : ControllerBase
    {
        private readonly IModuleTaskService _moduleTaskService;

        public ModuleTasksController(IModuleTaskService moduleTaskService)
        {
            _moduleTaskService = moduleTaskService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleTask>>> GetAllTasksModule()
        {
            try
            {
                var moduleTasks = await _moduleTaskService.GetAllTasksOfModuleAsync();
                return Ok(moduleTasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ModuleTask>> GetTaskOfModuleById(Guid id)
        {
            try
            {
                var moduleTask = await _moduleTaskService.GetTaskOfModuleByIdAsync(id);

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

        [HttpPost]
        public async Task<ActionResult> AddTaskToModule(CreateModuleTaskDTO moduleTaskDto)
        {
            try
            {
                await _moduleTaskService.AddTaskToModuleAsync(moduleTaskDto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTaskOfModule(
            Guid id, 
            [FromBody] UpdateModuleTaskDTO moduleTaskDto)
        {
            try
            {
                await _moduleTaskService.UpdateTaskOfModuleAsync(id, moduleTaskDto);
                return Ok();
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTaskOfModule(Guid id)
        {
            try
            {
                await _moduleTaskService.DeleteTaskOfModuleAsync(id);
                return NoContent();
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

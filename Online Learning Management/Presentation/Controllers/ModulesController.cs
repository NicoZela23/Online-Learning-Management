using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Management.Application.Modules.Responses;
using Online_Learning_Management.Domain.Entities.Modules;
using Online_Learning_Management.Domain.Exceptions.Module;
using Online_Learning_Management.Domain.Interfaces.Modules;
using Online_Learning_Management.Infrastructure.DTOs.Module;
using System.Linq.Expressions;

namespace Online_Learning_Management.Presentation.Controllers
{ 
    [Route("api/modules")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IModuleService _moduleService;
        public ModulesController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Module>>> GetAllModules([FromQuery] string search = null)
        {
            try
            {
                var modules = await _moduleService.GetAllModulesAsync(search);
                return Ok(modules);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Instructor")]
        public async Task<ActionResult<Module>> GetModuleById(Guid id)
        {
            try
            {
                var module = await _moduleService.GetModuleByIdAsync(id);
                return Ok(module);
            }
            catch (ModuleNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddModule(CreateModuleDTO moduleDto)
        {
            try
            {
                var createdModule = await _moduleService.AddModuleAsync(moduleDto);
                var response = new ApiResponse("User created successfully", createdModule);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateModule(Guid id, [FromBody] UpdateModuleDTO moduleDto)
        {
            try
            {
                var updatedModule = await _moduleService.UpdateModuleAsync(id, moduleDto);
                var response = new ApiResponse("User updated successfully", updatedModule);
                return Ok(response);
            }
            catch (ModuleNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ModuleValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteModule(Guid id)
        {
            try
            {
                await _moduleService.DeleteModuleAsync(id);
                return NoContent();
            }
            catch (ModuleNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Online_Learning_Management.Domain.Entities.Modules;
using Online_Learning_Management.Domain.Interfaces.Modules;
using Online_Learning_Management.Infrastructure.DTOs.Module;

namespace Online_Learning_Management.Presentation.Controllers
{
    [Route("modules")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IModuleService _moduleService;
        public ModulesController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Module>>> GetAllModules()
        {
            try
            {
                var modules = await _moduleService.GetAllModulesAsync();
                return Ok(modules);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Module>> GetModuleById(Guid id)
        {
            try
            {
                var module = await _moduleService.GetModuleByIdAsync(id);

                if (module == null)
                {
                    return NotFound();
                }

                return Ok(module);
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
                await _moduleService.AddModuleAsync(moduleDto);
                return StatusCode(201);
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
                await _moduleService.UpdateModuleAsync(id, moduleDto);
                return Ok();
            }
            catch (ArgumentException)
            {
                return NotFound("Module not found.");
            }
            catch (Exception ex)
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
            catch (ArgumentException)
            {
                return NotFound("Module not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

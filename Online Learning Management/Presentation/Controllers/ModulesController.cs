using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Management.Application.DTOs.Module;
using Online_Learning_Management.Domain.Entities;
using Online_Learning_Management.Domain.Entities.Module;
using Online_Learning_Management.Domain.Interfaces;
using Online_Learning_Management.Infrastructure.Repositories;

namespace Online_Learning_Management.Presentation.Controllers
{
    [Route("modules")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IModuleRepository _moduleRepository;
        public ModulesController(IModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Module>>> GetAllModules()
        {
            var modules = await _moduleRepository.GetAllModulesAsync();
            return Ok(modules);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Module>> GetModulesById(Guid id)
        {
            var module = await _moduleRepository.GetModuleByIdAsync(id);
            if (module == null)
            {
                return NotFound();
            }
            return Ok(module);
        }
        [HttpPost]
        public async Task<ActionResult> AddModule(Module module)
        {
            await _moduleRepository.AddModuleAsync(module);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateModule(Guid id, [FromBody] UpdateModuleDTO module)
        {
            var existingModule = await _moduleRepository.GetModuleByIdAsync(id);

            if (existingModule == null)
            {
                return NotFound("Module not found.");
            }
            existingModule.Name = module.Name;
            existingModule.Description = module.Description;

            await _moduleRepository.UpdateModuleAsync(existingModule);
            return StatusCode(200);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteModule(Guid id)
        {
            await _moduleRepository.DeleteModuleAsync(id);
            return NoContent();
        }
    }
}

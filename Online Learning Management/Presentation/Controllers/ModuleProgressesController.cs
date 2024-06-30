using Microsoft.AspNetCore.Mvc;
using Online_Learning_Management.Domain.Exceptions.ModuleProgress;
using Online_Learning_Management.Infrastructure.DTOs.ModuleProgresses;
using OnlineLearningManagement.Domain.Interfaces;


namespace Online_Learning_Management.Presentation.Controllers
{
    [ApiController]
    [Route("api/module-progresses")]
    public class ModuleProgressesController : ControllerBase
    {
        private readonly IModuleProgressService _moduleProgressServices;

        public ModuleProgressesController(IModuleProgressService moduleProgressServices)
        {
            _moduleProgressServices = moduleProgressServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllModuleProgresses()
        {
            try
            {
                var moduleProgresses = await _moduleProgressServices.GetAllModuleProgressesAsync();
                return Ok(moduleProgresses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetModuleProgressById(Guid id)
        {
            try
            {
                var moduleProgress = await _moduleProgressServices.GetModuleProgressByIdAsync(id);
                return Ok(moduleProgress);
            }
            catch (ModuleProgressNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddModuleProgress(CreateModuleProgressDTO createModuleProgressDTO)
        {
            try
            {
                var createdModuleProgress = await _moduleProgressServices.AddModuleProgressAsync(createModuleProgressDTO);
                return CreatedAtAction(nameof(GetModuleProgressById), new { id = createdModuleProgress.Id }, createdModuleProgress);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModuleProgress(Guid id, UpdateModuleProgressDTO updateModuleProgressDTO)
        {
            try
            {
                var updatedModuleProgress = await _moduleProgressServices.UpdateModuleProgressAsync(id, updateModuleProgressDTO);
                return Ok(updatedModuleProgress);
            }
            catch (ModuleProgressNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchModuleProgress(Guid id, PatchModuleProgressDTO progress)
        {
            try
            {
                var updatedModuleProgress = await _moduleProgressServices.PatchModuleProgressAsync(id, progress);
                return Ok(updatedModuleProgress);
            }
            catch (ModuleProgressNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModuleProgress(Guid id)
        {
            try
            {
                await _moduleProgressServices.DeleteModuleProgressAsync(id);
                return Ok(new { message = "ModuleProgress was deleted succesfully" });
            }
            catch (ModuleProgressNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
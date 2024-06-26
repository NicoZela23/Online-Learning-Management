using Microsoft.AspNetCore.Mvc;
using Online_Learning_Management.Domain.Exceptions.Auth;
using Online_Learning_Management.Domain.Interfaces.Instructors;

namespace Online_Learning_Management.Presentation.Controllers
{
    [Route("api/instructors")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private readonly INstructorService _instructorService;

        public InstructorsController(INstructorService instructorService)
        {
            _instructorService = instructorService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllInstructors()
        {
            try
            {
                var instructors = await _instructorService.GetAllInstructorsAsync();
                return Ok(instructors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInstructorById(Guid id)
        {
            try
            {
                var instructor = await _instructorService.GetInstructorByIdAsync(id);
                return Ok(instructor);
            }
            catch (InstructorNotFoundException ex)
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

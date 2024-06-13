using Microsoft.AspNetCore.Mvc;
using Online_Learning_Management.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Online_Learning_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsRepository _studentRepository;

        public StudentsController(IStudentsRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Students>> GetStudent(int id)
        {
            try
            {
                var student = await _studentRepository.GetStudentByIdAsync(id);

                if (student == null)
                {
                    return NotFound(new { message = $"Student with ID {id} not found." });
                }

                return Ok(student);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, new { message = "An error occurred while retrieving the student.", details = ex.Message });
            }
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            try
            {
                var student = await _studentRepository.GetStudentByIdAsync(id);
                if (student == null)
                {
                    return NotFound(new { message = $"Student with ID {id} not found." });
                }

                await _studentRepository.DeleteStudentByIdAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, new { message = "An error occurred while deleting the student.", details = ex.Message });
            }
        }
    }
}

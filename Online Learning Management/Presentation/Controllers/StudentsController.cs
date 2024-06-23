using Microsoft.AspNetCore.Mvc;
using Online_Learning_Management.Application.Students.Responses;
using Online_Learning_Management.Domain.Interfaces.Students;
using Online_Learning_Management.Infrastructure.DTOs.Student;
using System.Threading.Tasks;

namespace Online_Learning_Management.Presentation.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentServices _studentService;

        public StudentsController(IStudentServices studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            return Ok(student);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(CreateStudentDTO createStudentDTO)
        {
            
                var createdStudent = await _studentService.AddStudentAsync(createStudentDTO);
                var response = new StudentResponses(
                    "Student Created succesfully",
                    createdStudent
                    );
                return Ok(response);
           
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(Guid id, UpdateStudentDTO updateStudentDTO)
        {
            await _studentService.UpdateStudentAsync(id, updateStudentDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            try
            {
                await _studentService.DeleteStudentAsync(id);
                return Ok(new { message = "Student deleted successfully" });
            }
            catch (ArgumentException)
            {
                return NotFound("The Student does not exist.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }   
    }
}

using Microsoft.AspNetCore.Mvc;
using Online_Learning_Management.Domain.Interfaces.Students;
using Online_Learning_Management.Infrastructure.DTOs.Student;

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

        [HttpGet]
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
            await _studentService.AddStudentAsync(createStudentDTO);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent(Guid id, UpdateStudentDTO updateStudentDTO)
        {
            await _studentService.UpdateStudentAsync(id, updateStudentDTO);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            await _studentService.DeleteStudentAsync(id);
            return Ok();
        }   
    }
}

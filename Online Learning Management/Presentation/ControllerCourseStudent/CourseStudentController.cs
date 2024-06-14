using Microsoft.AspNetCore.Mvc;
using Online_Learning_Management.Domain.Interfaces.CourseStudents;
using Online_Learning_Management.Infrastructure.DTOs.CourseStudents;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace Online_Learning_Management.Presentation.ControllerCourseStudent
{  
     [ApiController]
    [Route("api/[controller]")]
 
    public class CourseStudentController : ControllerBase
    {
        private readonly ICourseStudentsService _courseStudentsService;

        public CourseStudentController(ICourseStudentsService courseStudentsService)
        {
            _courseStudentsService = courseStudentsService;
        }
            // POST: api/CourseStudent
        [HttpGet]
        public async Task<IEnumerable<CourseStudentDTO>> GetAllCourseStudentsAsync()
        {
            var courseStudents = await _courseStudentsService.GetAllCourseStudentsAsync();
            if(courseStudents == null)
            {
                return null;
            }
            return courseStudents;
            //return await _courseStudentsService.GetAllCourseStudentsAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseStudentByIdAsync(Guid id)
        {
            try
            {
                var courseStudent= await _courseStudentsService.GetCourseStudentByIdAsync(id);
                if(courseStudent == null)
                {
                    return NotFound(new { message = $"Student with ID {id} not found" });
                }
                return Ok(courseStudent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error", details=ex.Message });
            }
            //return await _courseStudentsService.GetCourseStudentByIdAsync(id);
        }

        [HttpDelete("{id}")]
        public async Task DeleteCourseStudentAsync(Guid id)
        {
            await _courseStudentsService.DeleteCourseStudentAsync(id);
        }
    }
}
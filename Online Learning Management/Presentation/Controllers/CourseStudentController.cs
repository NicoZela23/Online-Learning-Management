using Microsoft.AspNetCore.Mvc;
using Online_Learning_Management.Domain.Interfaces.CourseStudents;
using Online_Learning_Management.Infrastructure.DTOs.CourseStudents;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Learning_Management.Presentation.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetAllCourseStudentsAsync()
        {
            try
            {
                var courseStudents = await _courseStudentsService.GetAllCourseStudentsAsync();
                if (courseStudents == null)
                {
                    return NotFound(new { message = "No course students found" });
                }
                return Ok(courseStudents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving course students", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseStudentByIdAsync(Guid id)
        {
            try
            {
                var courseStudent = await _courseStudentsService.GetCourseStudentByIdAsync(id);
                if (courseStudent == null)
                {
                    return NotFound(new { message = $"Course student with ID {id} not found" });
                }
                return Ok(courseStudent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving course student", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseStudentAsync(Guid id)
        {
            try
            {
                await _courseStudentsService.DeleteCourseStudentAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting course student", details = ex.Message });
            }
        }
    }
}

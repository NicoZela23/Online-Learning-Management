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
                return NoContent(new { message = "Course student has been deleted" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting course student", details = ex.Message });
            }
        }

        // new method to withdraw a student from a course

        [HttpPost("withdraw")] // POST api/coursestudent/withdraw
        public async Task<IActionResult> WithdrawCourseStudentAsync([FromBody] WithdrawCourseStudentRequest request)
        {
            try
            {
                await _courseStudentsService.WithdrawCourseStudentAsync(request.StudentId, request.CourseId);
                return NoContent(new { message = "Student has been withdrawn from the course" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while withdrawing from the course", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EnrollStudent(EnrollStudentDTO enrollStudentDTO)
        {
            try
            {
                await _courseStudentsService.EnrollCourseStudentAsync(enrollStudentDTO);
                return Ok("Student enrolled successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex) when (ex.Message == "The student is already enrolled in the course")
            {
                return StatusCode(409, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private IActionResult NoContent(object value)
        {
            throw new NotImplementedException();
        }
    }
}


public class WithdrawCourseStudentRequest
{
    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }
}
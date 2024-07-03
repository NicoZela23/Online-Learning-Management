
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Management.Domain.Entities;
using Online_Learning_Management.Domain.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Online_Learning_Management.Presentation.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {

        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourseDTO courseDto)
        {
            try
            {
                var course = await _courseService.CreateCourseAsync(courseDto);
                return CreatedAtAction(nameof(GetCourseById), new { Id = course.Id }, new { message = "Course created successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to create course.", ex.Message });
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCourseById(Guid Id)
        {
            try
            {
                var course = await _courseService.GetCourseByIdAsync(Id);
                return Ok(course);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{courseId}")]
        public async Task<IActionResult> UpdateCourse(Guid courseId, UpdateCourseDTO courseDto)
        {
            try
            {
                var updatedCourse = await _courseService.UpdateCourseAsync(courseId, courseDto);
                return Ok(new { message = "Course updated successfully.", updatedCourse });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = "Course not found." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to update course.", ex.Message });
            }
        }


        [HttpDelete("{courseId}")]
        public async Task<IActionResult> Delete(Guid courseId)
        {
            var course = await _courseService.GetCourseByIdAsync(courseId);
            if (course == null)
            {
                return NotFound();
            }

            await _courseService.DeleteCourseAsync(courseId);
            return Ok(new { message = "Course was deleted succesfully" });
        }

        [HttpGet("/instructors/{IdInstructor}/courses")]
        public async Task<IActionResult> GetCoursesByIdInstructor(int IdInstructor)
        {
            try
            {
                var courses = await _courseService.GetCoursesByIdInstructorAsync(IdInstructor);
                return Ok(courses);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }

    }
}

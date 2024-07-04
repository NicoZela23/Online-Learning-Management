
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
                return CreatedAtAction(nameof(GetCourseById), new { Id = course.Id }, course);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                return Ok(updatedCourse);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{courseId}")]
        public async Task<IActionResult> Delete(Guid courseId)
        {
           try
            {
            await _courseService.DeleteCourseAsync(courseId);   
            return Ok(new { message = "Course was deleted succesfully" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

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

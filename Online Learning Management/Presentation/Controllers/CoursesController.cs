
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Management.Domain.Entities;
using Online_Learning_Management.Domain.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Online_Learning_Management.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CoursesController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] Course course)
        {
            if (course == null || string.IsNullOrEmpty(course.Title))
                return BadRequest("Invalid course data");

            var createdCourse = await _courseRepository.CreateCourseAsync(course);
            return CreatedAtAction(nameof(GetCourseById), new { courseId = createdCourse.Id }, createdCourse);
        }

        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetCourseById(int courseId)
        {
            var course = await _courseRepository.GetCourseByIdAsync(courseId);
            if (course == null)
                return NotFound();

            return Ok(course);
        }

    }
}


using Microsoft.AspNetCore.Mvc;
using Online_Learning_Management.Domain.Entities;
using Online_Learning_Management.Domain.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Online_Learning_Management.Presentation.Controllers
{
    [ApiController]
    [Route("courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CoursesController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(Course course)
        {
            if (course == null || string.IsNullOrEmpty(course.Title))
                return BadRequest("Invalid course data");

            var createdCourse = await _courseRepository.CreateCourseAsync(course);
            return CreatedAtAction(nameof(GetCourseById), new { Id = createdCourse.Id }, createdCourse);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCourseById(Guid Id)
        {
            var course = await _courseRepository.GetCourseByIdAsync(Id);
            if (course == null)
                return NotFound();

            return Ok(course);
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Management.Domain.Interfaces.CourseStudents;
using Online_Learning_Management.Domain.Interfaces.ReportCourses;

namespace Online_Learning_Management.Presentation.Controllers
{

    [ApiController]
    [Route("course/report")]
    public class ReportCoursesController : Controller
    {
        private readonly IReportCourseService _reportCourseService;

        public ReportCoursesController(IReportCourseService reportCourseService)
        {
            _reportCourseService = reportCourseService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReportCourseByIdAsync(Guid id)
        {
            try
            {
                var reportCourse = await _reportCourseService.GetReportCourseByIdAsync(id);
                if (reportCourse == null)
                {
                    return NotFound(new { message = $"Course with ID {id} not found" });
                }
                return Ok(reportCourse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving course", details = ex.Message });
            }
        }
    }
}

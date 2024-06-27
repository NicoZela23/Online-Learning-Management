using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Management.Domain.Interfaces.ReportCourses;

namespace Online_Learning_Management.Presentation.Controllers
{

    [ApiController]
    [Route("courses/{courseId}/report")]
    public class ReportCoursesController : Controller
    {
        private readonly IReportCourseService _reportCourseService;

        public ReportCoursesController(IReportCourseService reportCourseService)
        {
            _reportCourseService = reportCourseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReportCourseByIdAsync(Guid courseId, Guid studentId)
        {
            if (courseId == Guid.Empty || studentId == Guid.Empty)
            {
                return BadRequest(new { message = "Course ID and Student ID must be provided and valid" });
            }

            try
            {
                var reportCourse = await _reportCourseService.GetReportCourseByStudentAndCourseAsync(studentId, courseId);
                if (reportCourse == null)
                {
                    return NotFound(new { message = $"No progress report found for Student ID {studentId} in Course ID {courseId}" });
                }
                return Ok(reportCourse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the progress report", details = ex.Message });
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities.ReportCourses;
using Online_Learning_Management.Infrastructure.Data;
using Online_Learning_Management.Infrastructure.Repositories.ReportCourses;

namespace OLM_Tests.Repository.ReportCourses
{
    public class CourseReportsTest
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task GetReportCourseByStudentAndCourseAsync_ShouldReturnReportCourse()
        {
            var context = GetDbContext();
            var repository = new ReportCourseRepository(context);
            var studentId = Guid.NewGuid();
            var courseId = Guid.NewGuid();
            var reportCourse = new ReportCourse
            {
                Id = Guid.NewGuid(),
                StudentID = studentId,
                CourseID = courseId,
                CourseName = "Modules general",
                StudentName = "John Doe",
                ProgressPercentage = 90
            };
            await context.ReportCourses.AddAsync(reportCourse);
            await context.SaveChangesAsync();

            var result = await repository.GetReportCourseByStudentAndCourseAsync(studentId, courseId);

            Assert.NotNull(result);
            Assert.Equal(reportCourse.Id, result.Id);
            Assert.Equal(reportCourse.StudentID, result.StudentID);
            Assert.Equal(reportCourse.CourseID, result.CourseID);
            Assert.Equal(reportCourse.ProgressPercentage, result.ProgressPercentage);
            Assert.Equal(reportCourse.StudentName, result.StudentName);
            Assert.Equal(reportCourse.CourseName, result.CourseName);
        }

        [Fact]
        public async Task GetReportCourseByStudentAndCourseAsync_ShouldReturnNullIfNotFound()
        {
            var context = GetDbContext();
            var repository = new ReportCourseRepository(context);
            var studentId = Guid.NewGuid();
            var courseId = Guid.NewGuid();

            var result = await repository.GetReportCourseByStudentAndCourseAsync(studentId, courseId);

            Assert.Null(result);
        }
    }
}

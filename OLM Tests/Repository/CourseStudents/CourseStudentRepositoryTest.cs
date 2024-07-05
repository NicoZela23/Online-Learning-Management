using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities.CourseStudent;
using Online_Learning_Management.Domain.Entities.Students;
using Online_Learning_Management.Infrastructure.Data;
using Online_Learning_Management.Infrastructure.Repositories.CourseStudents;

namespace OLM_Tests.Repository.CourseStudents
{
    public class CourseStudentRepositoryTest
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        private CourseStudent CreateTestCourseStudent(Guid studentId, Guid courseId)
        {
            return new CourseStudent
            {
                Id = Guid.NewGuid(),
                StudentID = studentId,
                CourseID = courseId,
                EnrollmentDate = DateTime.UtcNow
            };
        }

        [Fact]
        public async Task AddCourseStudentAsync_ShouldAddCourseStudent()
        {
            var context = GetDbContext();
            var repository = new CourseStudentsRepository(context);
            var courseStudent = CreateTestCourseStudent(Guid.NewGuid(), Guid.NewGuid());

            await repository.AddCourseStudentAsync(courseStudent);

            var result = await context.CourseStudents.FindAsync(courseStudent.Id);
            Assert.NotNull(result);
            Assert.Equal(courseStudent.StudentID, result.StudentID);
        }

        [Fact]
        public async Task GetCourseStudentByIdAsync_ShouldReturnCourseStudent()
        {
            var context = GetDbContext();
            var courseStudent = CreateTestCourseStudent(Guid.NewGuid(), Guid.NewGuid());
            await context.CourseStudents.AddAsync(courseStudent);
            await context.SaveChangesAsync();

            var repository = new CourseStudentsRepository(context);
            var result = await repository.GetCourseStudentByIdAsync(courseStudent.Id);

            Assert.NotNull(result);
            Assert.Equal(courseStudent.StudentID, result.StudentID);
        }

        [Fact]
        public async Task GetAllCourseStudentsAsync_ShouldReturnAllCourseStudents()
        {
            var context = GetDbContext();
            var courseStudent1 = CreateTestCourseStudent(Guid.NewGuid(), Guid.NewGuid());
            var courseStudent2 = CreateTestCourseStudent(Guid.NewGuid(), Guid.NewGuid());
            await context.CourseStudents.AddRangeAsync(courseStudent1, courseStudent2);
            await context.SaveChangesAsync();

            var repository = new CourseStudentsRepository(context);
            var result = await repository.GetAllCourseStudentsAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task DeleteCourseStudentAsync_ShouldRemoveCourseStudent()
        {
            var context = GetDbContext();
            var courseStudent = CreateTestCourseStudent(Guid.NewGuid(), Guid.NewGuid());
            await context.CourseStudents.AddAsync(courseStudent);
            await context.SaveChangesAsync();

            var repository = new CourseStudentsRepository(context);
            await repository.DeleteCourseStudentAsync(courseStudent.Id);

            var result = await context.CourseStudents.FindAsync(courseStudent.Id);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetCourseStudentByStudentAndCourseAsync_ShouldReturnCourseStudent()
        {
            var context = GetDbContext();
            var studentId = Guid.NewGuid();
            var courseId = Guid.NewGuid();
            var courseStudent = CreateTestCourseStudent(studentId, courseId);
            await context.CourseStudents.AddAsync(courseStudent);
            await context.SaveChangesAsync();

            var repository = new CourseStudentsRepository(context);
            var result = await repository.GetCourseStudentByStudentAndCourseAsync(studentId, courseId);

            Assert.NotNull(result);
            Assert.Equal(courseStudent.Id, result.Id);
        }

        [Fact]
        public async Task CourseExistsAsync_ShouldReturnTrueIfCourseExists()
        {
            var context = GetDbContext();
            var courseId = Guid.NewGuid();
            var course = new Online_Learning_Management.Domain.Entities.Courses.Course
            {
                Id = courseId,
                Title = "Test Course",
                Description = "Test Description",
                IdInstructor = Guid.NewGuid(),
                Content = new List<string> { "Introduction" },
                DurationInWeeks = 5
            };
            await context.Courses.AddAsync(course);
            await context.SaveChangesAsync();

            var repository = new CourseStudentsRepository(context);
            var result = await repository.CourseExistsAsync(courseId);

            Assert.True(result);
        }

        [Fact]
        public async Task CourseExistsAsync_ShouldReturnFalseIfCourseDoesNotExist()
        {
            var context = GetDbContext();
            var courseId = Guid.NewGuid();

            var repository = new CourseStudentsRepository(context);
            var result = await repository.CourseExistsAsync(courseId);

            Assert.False(result);
        }

        [Fact]
        public async Task StudentExistsAsync_ShouldReturnTrueIfStudentExists()
        {
            var context = GetDbContext();
            var studentId = Guid.NewGuid();
            var student = new Student
            {
                Id = studentId,
                UserId = studentId,
                Name = "student",
                LastName = "User",
                Email = "student@example.com",
                CreateAt = DateTime.UtcNow
            };
            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();

            var repository = new CourseStudentsRepository(context);
            var result = await repository.StudentExistsAsync(studentId);

            Assert.True(result);
        }

        [Fact]
        public async Task StudentExistsAsync_ShouldReturnFalseIfStudentDoesNotExist()
        {
            var context = GetDbContext();
            var studentId = Guid.NewGuid();

            var repository = new CourseStudentsRepository(context);
            var result = await repository.StudentExistsAsync(studentId);

            Assert.False(result);
        }
    }
}

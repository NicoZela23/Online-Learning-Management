using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities.Courses;
using Online_Learning_Management.Infrastructure.Data;
using Online_Learning_Management.Infrastructure.Repositories.Courses;

namespace OLM_Tests.Repository.Courses
{
    public class CoursesRepositoryTest
    {
        private ApplicationDbContext GetDbContext()
        {

           var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        private Course CreateTestCourse(Guid instructorId)
        {
            return new Course
            {
                Id = Guid.NewGuid(),
                Title = "Test Course",
                Description = "This is a test course.",
                IdInstructor = instructorId,
                Content = new List<string> { "Introduction", "Chapter 1" },
                DurationInWeeks = 5
            };
        }

        [Fact]
        public async Task CreateCourseAsync_ShouldCreateCourse()
        {
            var context = GetDbContext();
            var repository = new CourseRepository(context);
            var course = CreateTestCourse(Guid.NewGuid());

            var result = await repository.CreateCourseAsync(course);

            Assert.NotNull(result);
            Assert.Equal("Test Course", result.Title);
            Assert.Single(context.Courses);
        }

        [Fact]
        public async Task GetCourseByIdAsync_ShouldReturnCourse()
        {
            var context = GetDbContext();
            var course = CreateTestCourse(Guid.NewGuid());
            await context.Courses.AddAsync(course);
            await context.SaveChangesAsync();

            var repository = new CourseRepository(context);
            var result = await repository.GetCourseByIdAsync(course.Id);

            Assert.NotNull(result);
            Assert.Equal("Test Course", result.Title);
        }

        [Fact]
        public async Task GetCoursesByIdInstructorAsync_ShouldReturnCourses()
        {
            var context = GetDbContext();
            var coursesId = Guid.NewGuid();
            var course1 = CreateTestCourse(coursesId);
            var course2 = CreateTestCourse(coursesId);
            var course3 = CreateTestCourse(Guid.NewGuid());
            await context.Courses.AddRangeAsync(course1, course2, course3);
            await context.SaveChangesAsync();

            var repository = new CourseRepository(context);
            var result = await repository.GetCoursesByIdInstructorAsync(coursesId);

            Assert.Equal(2, result.Count());
            Assert.All(result, course => Assert.Equal(coursesId, course.IdInstructor));
        }

        [Fact]
        public async Task InstructorExistsAsync_ShouldReturnFalseIfInstructorDoesNotExist()
        {
            var context = GetDbContext();
            var course = CreateTestCourse(Guid.NewGuid());
            await context.Courses.AddAsync(course);
            await context.SaveChangesAsync();

            var repository = new CourseRepository(context);
            var result = await repository.InstructorExistsAsync(Guid.NewGuid());

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateCourseAsync_ShouldUpdateCourse()
        {
            var context = GetDbContext();
            var course = CreateTestCourse(Guid.NewGuid());
            await context.Courses.AddAsync(course);
            await context.SaveChangesAsync();

            course.Title = "Updated Course";
            var repository = new CourseRepository(context);
            var result = await repository.UpdateCourseAsync(course);

            Assert.NotNull(result);
            Assert.Equal("Updated Course", result.Title);
        }

        [Fact]
        public async Task DeleteCourseAsync_ShouldRemoveCourse()
        {
            var context = GetDbContext();
            var course = CreateTestCourse(Guid.NewGuid());
            await context.Courses.AddAsync(course);
            await context.SaveChangesAsync();

            var repository = new CourseRepository(context);
            await repository.DeleteCourseAsync(course.Id);

            Assert.Empty(context.Courses);
        }
    }
}

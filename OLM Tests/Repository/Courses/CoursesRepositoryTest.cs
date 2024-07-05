using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities.Courses;
using Online_Learning_Management.Infrastructure.Data;
using Online_Learning_Management.Infrastructure.Repositories.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private Course CreateTestCourse(int instructorId)
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
            var course = CreateTestCourse(1);

            var result = await repository.CreateCourseAsync(course);

            Assert.NotNull(result);
            Assert.Equal("Test Course", result.Title);
            Assert.Single(context.Courses);
        }

        [Fact]
        public async Task GetCourseByIdAsync_ShouldReturnCourse()
        {
            var context = GetDbContext();
            var course = CreateTestCourse(1);
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
            var course1 = CreateTestCourse(1);
            var course2 = CreateTestCourse(1);
            var course3 = CreateTestCourse(2);
            await context.Courses.AddRangeAsync(course1, course2, course3);
            await context.SaveChangesAsync();

            var repository = new CourseRepository(context);
            var result = await repository.GetCoursesByIdInstructorAsync(1);

            Assert.Equal(2, result.Count());
            Assert.All(result, course => Assert.Equal(1, course.IdInstructor));
        }

        [Fact]
        public async Task InstructorExistsAsync_ShouldReturnTrueIfInstructorExists()
        {
            var context = GetDbContext();
            var course = CreateTestCourse(1);
            await context.Courses.AddAsync(course);
            await context.SaveChangesAsync();

            var repository = new CourseRepository(context);
            var result = await repository.InstructorExistsAsync(1);

            Assert.True(result);
        }

        [Fact]
        public async Task InstructorExistsAsync_ShouldReturnFalseIfInstructorDoesNotExist()
        {
            var context = GetDbContext();
            var course = CreateTestCourse(1);
            await context.Courses.AddAsync(course);
            await context.SaveChangesAsync();

            var repository = new CourseRepository(context);
            var result = await repository.InstructorExistsAsync(2);

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateCourseAsync_ShouldUpdateCourse()
        {
            var context = GetDbContext();
            var course = CreateTestCourse(1);
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
            var course = CreateTestCourse(1);
            await context.Courses.AddAsync(course);
            await context.SaveChangesAsync();

            var repository = new CourseRepository(context);
            await repository.DeleteCourseAsync(course.Id);

            Assert.Empty(context.Courses);
        }
    }
}

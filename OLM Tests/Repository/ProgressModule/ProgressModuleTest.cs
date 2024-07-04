using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities.Auth;
using Online_Learning_Management.Domain.Entities.Courses;
using Online_Learning_Management.Domain.Entities.ModuleProgresses;
using Online_Learning_Management.Domain.Entities.Modules;
using Online_Learning_Management.Domain.Entities.Students;
using Online_Learning_Management.Infrastructure.Data;
using Online_Learning_Management.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM_Tests.Repository.ProgressModule
{
    public class ProgressModuleTest
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        private ModuleProgress CreateTestModuleProgress(Guid courseId, Guid moduleId, Guid studentId)
        {
            return new ModuleProgress
            {
                Id = Guid.NewGuid(),
                CourseId = courseId,
                ModuleId = moduleId,
                StudentId = studentId,
                Progress = "In Progress"
            };
        }

        [Fact]
        public async Task AddModuleProgressAsync_ShouldAddModuleProgress()
        {
            var context = GetDbContext();
            var repository = new ModuleProgressesRepository(context);
            var moduleProgress = CreateTestModuleProgress(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            await repository.AddModuleProgressAsync(moduleProgress);

            var result = await context.ModuleProgresses.FindAsync(moduleProgress.Id);
            Assert.NotNull(result);
            Assert.Equal(moduleProgress.CourseId, result.CourseId);
        }

        [Fact]
        public async Task GetModuleProgressByIdAsync_ShouldReturnModuleProgress()
        {
            var context = GetDbContext();
            var moduleProgress = CreateTestModuleProgress(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            await context.ModuleProgresses.AddAsync(moduleProgress);
            await context.SaveChangesAsync();

            var repository = new ModuleProgressesRepository(context);
            var result = await repository.GetModuleProgressByIdAsync(moduleProgress.Id);

            Assert.NotNull(result);
            Assert.Equal(moduleProgress.CourseId, result.CourseId);
        }

        [Fact]
        public async Task GetAllModuleProgressesAsync_ShouldReturnAllModuleProgresses()
        {
            var context = GetDbContext();
            var moduleProgress1 = CreateTestModuleProgress(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            var moduleProgress2 = CreateTestModuleProgress(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            await context.ModuleProgresses.AddRangeAsync(moduleProgress1, moduleProgress2);
            await context.SaveChangesAsync();

            var repository = new ModuleProgressesRepository(context);
            var result = await repository.GetAllModuleProgressesAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task UpdateModuleProgressAsync_ShouldUpdateModuleProgress()
        {
            var context = GetDbContext();
            var moduleProgress = CreateTestModuleProgress(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            await context.ModuleProgresses.AddAsync(moduleProgress);
            await context.SaveChangesAsync();

            var repository = new ModuleProgressesRepository(context);
            moduleProgress.Progress = "Completed";
            await repository.UpdateModuleProgressAsync(moduleProgress);

            var updatedModuleProgress = await context.ModuleProgresses.FindAsync(moduleProgress.Id);
            Assert.NotNull(updatedModuleProgress);
            Assert.Equal("Completed", updatedModuleProgress.Progress);
        }

        [Fact]
        public async Task DeleteModuleProgressAsync_ShouldRemoveModuleProgress()
        {
            var context = GetDbContext();
            var moduleProgress = CreateTestModuleProgress(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            await context.ModuleProgresses.AddAsync(moduleProgress);
            await context.SaveChangesAsync();

            var repository = new ModuleProgressesRepository(context);
            await repository.DeleteModuleProgressAsync(moduleProgress.Id);

            var result = await context.ModuleProgresses.FindAsync(moduleProgress.Id);
            Assert.Null(result);
        }

        [Fact]
        public async Task ModuleProgressExistsAsync_ShouldCheckModuleProgressExistence()
        {
            var context = GetDbContext();
            var courseId = Guid.NewGuid();
            var moduleId = Guid.NewGuid();
            var studentId = Guid.NewGuid();
            var moduleProgress = CreateTestModuleProgress(courseId, moduleId, studentId);
            await context.ModuleProgresses.AddAsync(moduleProgress);
            await context.SaveChangesAsync();

            var repository = new ModuleProgressesRepository(context);
            var result = await repository.ModuleProgressExistsAsync(courseId, moduleId, studentId);

            Assert.True(result);
        }

        [Fact]
        public async Task CourseExistsAsync_ShouldCheckCourseExistence()
        {
            var context = GetDbContext();
            var courseId = Guid.NewGuid();
            var course = new Course
            {
                Id = courseId,
                Title = "TestTitle",
                IdInstructor = 23,
                Description = "TestDescription",
                Content = new List<string> { "Content1", "Content2" },
                DurationInWeeks = 12
            };

            await context.Courses.AddAsync(course);
            await context.SaveChangesAsync();
            var repository = new ModuleProgressesRepository(context);
            var result = await repository.CourseExistsAsync(courseId);
            Assert.True(result, $"Course with Id {courseId} should exist.");
        }

        [Fact]
        public async Task ModuleExistsAsync_ShouldCheckModuleExistence()
        {
            var context = GetDbContext();
            var moduleId = Guid.NewGuid();
            var module = new Module
            {
                Id = moduleId,
                CourseID = Guid.NewGuid(),
                Name = "Test Module",
                Description = "Test Description",
                Duration = TimeSpan.FromHours(20),
                LearningOutcomes = new List<string> { "Outcome 1", "Outcome 2" },
                Prerequisites = new List<string> { "Prerequisite 1", "Prerequisite 2" },
                StartDate = new DateOnly(2024, 1, 1),
                EndDate = new DateOnly(2024, 12, 31),
                Resources = new List<string> { "Resource 1", "Resource 2" },
                AssessmentMethods = new Dictionary<string, int> { { "Method 1", 80 }, { "Method 2", 90 } }
            };

            await context.Modules.AddAsync(module);
            await context.SaveChangesAsync();
            var repository = new ModuleProgressesRepository(context);
            var result = await repository.ModuleExistsAsync(moduleId);
            Assert.True(result, $"Module with Id {moduleId} should exist.");
        }

        [Fact]
        public async Task StudentExistsAsync_ShouldCheckStudentExistence()
        {
            var context = GetDbContext();
            var studentId = Guid.NewGuid();
            var student = new Student
            {
                Id = studentId,
                UserId = Guid.NewGuid(),
                Name = "Test Student",
                LastName = "Test Last Name",
                Email = "test@example.com",
                CreateAt = DateTime.UtcNow
            };
            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();

            var repository = new ModuleProgressesRepository(context);
            var result = await repository.StudentExistsAsync(studentId);

            Assert.True(result, $"Student with Id {studentId} should exist.");
        }

    }
}

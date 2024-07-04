using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities.Instructors;
using Online_Learning_Management.Infrastructure.Data;
using Online_Learning_Management.Infrastructure.Repositories.Instructors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM_Tests.Repository.Instructors
{
    public class IntructorRepositoryTest
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        private Instructor CreateTestInstructor()
        {
            return new Instructor
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Test",
                LastName = "Instructor",
                Email = "test@example.com",
                CreateAt = DateTime.Now
            };
        }

        [Fact]
        public async Task AddInstructorAsync_ShouldAddInstructor()
        {
            var context = GetDbContext();
            var repository = new InstructorRepository(context);
            var instructor = CreateTestInstructor();

            await repository.AddInstructorAsync(instructor);

            var result = await context.Instructors.FindAsync(instructor.Id);
            Assert.NotNull(result);
            Assert.Equal(instructor.Name, result.Name);
        }

        [Fact]
        public async Task GetInstructorByIdAsync_ShouldReturnInstructor()
        {
            var context = GetDbContext();
            var instructor = CreateTestInstructor();
            await context.Instructors.AddAsync(instructor);
            await context.SaveChangesAsync();

            var repository = new InstructorRepository(context);
            var result = await repository.GetInstructorByIdAsync(instructor.Id);

            Assert.NotNull(result);
            Assert.Equal(instructor.Name, result.Name);
        }

        [Fact]
        public async Task GetAllInstructorsAsync_ShouldReturnAllInstructors()
        {
            var context = GetDbContext();
            var instructor1 = CreateTestInstructor();
            var instructor2 = CreateTestInstructor();
            await context.Instructors.AddRangeAsync(instructor1, instructor2);
            await context.SaveChangesAsync();

            var repository = new InstructorRepository(context);
            var result = await repository.GetAllInstructorsAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task UpdateInstructorAsync_ShouldUpdateInstructor()
        {
            var context = GetDbContext();
            var instructor = CreateTestInstructor();
            await context.Instructors.AddAsync(instructor);
            await context.SaveChangesAsync();

            var repository = new InstructorRepository(context);
            instructor.Name = "Updated Name";
            await repository.UpdateInstructorAsync(instructor);

            var updatedInstructor = await context.Instructors.FindAsync(instructor.Id);
            Assert.NotNull(updatedInstructor);
            Assert.Equal("Updated Name", updatedInstructor.Name);
        }

        [Fact]
        public async Task DeleteInstructorAsync_ShouldRemoveInstructor()
        {
            var context = GetDbContext();
            var instructor = CreateTestInstructor();
            await context.Instructors.AddAsync(instructor);
            await context.SaveChangesAsync();

            var repository = new InstructorRepository(context);
            await repository.DeleteInstructorAsync(instructor.Id);

            var result = await context.Instructors.FindAsync(instructor.Id);
            Assert.Null(result);
        }
    }
}

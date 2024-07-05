using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities.Modules;
using Online_Learning_Management.Infrastructure.Data;
using Online_Learning_Management.Infrastructure.Repositories.Modules;

namespace OLM_Tests.Repository.Modules
{
    public class ModuleRepositoryTest
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        private Module CreateTestModule()
        {
            return new Module
            {
                Id = Guid.NewGuid(),
                CourseID = Guid.NewGuid(),
                Name = "Introduction to Programming",
                Description = "Learn the basics of programming.",
                Duration = TimeSpan.FromHours(20),
                LearningOutcomes = new List<string> { "Write simple programs", "Understand variables and loops" },
                Prerequisites = new List<string> { "Basic computer literacy" },
                StartDate = DateOnly.FromDateTime(DateTime.Now.Date),
                EndDate = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(30)),
                Resources = new List<string> { "Online tutorials", "Code samples" },
                AssessmentMethods = new Dictionary<string, int>
    {
        { "Quizzes", 30 },
        { "Final project", 70 }
    }
            };
        }

        [Fact]
        public async Task AddModuleAsync_ShouldAddModule()
        {
            var context = GetDbContext();
            var repository = new ModuleRepository(context);
            var module = CreateTestModule();

            await repository.AddModuleAsync(module);

            var result = await context.Modules.FindAsync(module.Id);
            Assert.NotNull(result);
            Assert.Equal(module.Name, result.Name);
        }

        [Fact]
        public async Task GetModuleByIdAsync_ShouldReturnModule()
        {
            var context = GetDbContext();
            var module = CreateTestModule();
            await context.Modules.AddAsync(module);
            await context.SaveChangesAsync();

            var repository = new ModuleRepository(context);
            var result = await repository.GetModuleByIdAsync(module.Id);

            Assert.NotNull(result);
            Assert.Equal(module.Name, result.Name);
        }

        [Fact]
        public async Task GetAllModulesAsync_ShouldReturnAllModules()
        {
            var context = GetDbContext();
            var module1 = CreateTestModule();
            var module2 = CreateTestModule();
            await context.Modules.AddRangeAsync(module1, module2);
            await context.SaveChangesAsync();

            var repository = new ModuleRepository(context);
            var result = await repository.GetAllModulesAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task UpdateModuleAsync_ShouldUpdateModule()
        {
            var context = GetDbContext();
            var module = CreateTestModule();
            await context.Modules.AddAsync(module);
            await context.SaveChangesAsync();

            var repository = new ModuleRepository(context);
            module.Name = "Updated Title";
            await repository.UpdateModuleAsync(module);

            var updatedModule = await context.Modules.FindAsync(module.Id);
            Assert.NotNull(updatedModule);
            Assert.Equal("Updated Title", updatedModule.Name);
        }

        [Fact]
        public async Task DeleteModuleAsync_ShouldRemoveModule()
        {
            var context = GetDbContext();
            var module = CreateTestModule();
            await context.Modules.AddAsync(module);
            await context.SaveChangesAsync();

            var repository = new ModuleRepository(context);
            await repository.DeleteModuleAsync(module.Id);

            var result = await context.Modules.FindAsync(module.Id);
            Assert.Null(result);
        }
    }
}

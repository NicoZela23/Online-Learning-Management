using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities.ModuleTasks;
using Online_Learning_Management.Infrastructure.Data;
using Online_Learning_Management.Infrastructure.Repositories.ModuleTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM_Tests.Repository.ModuleTasks
{
    public class ModuleTasksTest
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task AddTaskToModuleAsync_ShouldAddTask()
        {
            var context = GetDbContext();
            var repository = new ModuleTaskRepository(context);
            var moduleTask = new ModuleTask
            {
                Id = Guid.NewGuid(),
                ModuleID = Guid.NewGuid(),
                Title = "Test Task",
                Description = "Task description",
                Type = "Assignment",
                DateCreated = DateOnly.FromDateTime(DateTime.UtcNow),
                Deadline = DateTime.UtcNow.AddDays(7)
            };

            var result = await repository.AddTaskToModuleAsync(moduleTask);

            Assert.NotNull(result);
            Assert.Equal(moduleTask.Id, result.Id);
            Assert.Equal(moduleTask.ModuleID, result.ModuleID);
            Assert.Equal(moduleTask.Title, result.Title);
            Assert.Equal(moduleTask.Description, result.Description);
            Assert.Equal(moduleTask.Deadline, result.Deadline);
        }

        [Fact]
        public async Task GetAllTasksOfModuleAsync_ShouldRetrieveAllTasks()
        {
            var context = GetDbContext();
            var repository = new ModuleTaskRepository(context);
            var moduleID = Guid.NewGuid();

            var tasks = new List<ModuleTask>
            {
                new ModuleTask { Id = Guid.NewGuid(), ModuleID = moduleID, Title = "Task 1", Description = "Task description", Type = "Assignment", DateCreated = DateOnly.FromDateTime(DateTime.UtcNow), Deadline = DateTime.UtcNow.AddDays(7) },
                new ModuleTask { Id = Guid.NewGuid(), ModuleID = moduleID, Title = "Task 2", Description = "Task description 2", Type = "Assignment 2", DateCreated = DateOnly.FromDateTime(DateTime.UtcNow), Deadline = DateTime.UtcNow.AddDays(7) },
            };
            await context.ModuleTasks.AddRangeAsync(tasks);
            await context.SaveChangesAsync();

            var result = await repository.GetAllTasksOfModuleAsync(moduleID);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, t => t.Title == "Task 1");
            Assert.Contains(result, t => t.Title == "Task 2");
        }

        [Fact]
        public async Task GetTaskOfModuleByIdAsync_ShouldRetrieveTaskById()
        {
            var context = GetDbContext();
            var repository = new ModuleTaskRepository(context);
            var taskId = Guid.NewGuid();
            var moduleTask = new ModuleTask
            {
                Id = taskId,
                ModuleID = Guid.NewGuid(),
                Title = "Task 1",
                Description = "Task description",
                Type = "Assignment",
                DateCreated = DateOnly.FromDateTime(DateTime.UtcNow),
                Deadline = DateTime.UtcNow.AddDays(7)
            };
            await context.ModuleTasks.AddAsync(moduleTask);
            await context.SaveChangesAsync();

            var result = await repository.GetTaskOfModuleByIdAsync(taskId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(taskId, result.Id);
            Assert.Equal("Task 1", result.Title);
            Assert.Equal("Task description", result.Description);
            Assert.Equal("Assignment", result.Type);
        }


        [Fact]
        public async Task UpdateTaskOfModuleAsync_ShouldUpdateTask()
        {
            var context = GetDbContext();
            var repository = new ModuleTaskRepository(context);
            var taskId = Guid.NewGuid();
            var moduleTask = new ModuleTask
            {
                Id = taskId,
                ModuleID = Guid.NewGuid(),
                Title = "Task 1",
                Description = "Task description",
                Type = "Assignment",
                DateCreated = DateOnly.FromDateTime(DateTime.UtcNow),
                Deadline = DateTime.UtcNow.AddDays(7)
            };
            await context.ModuleTasks.AddAsync(moduleTask);
            await context.SaveChangesAsync();

            moduleTask.Title = "Updated Task";

            await repository.UpdateTaskOfModuleAsync(moduleTask);
            var updatedTask = await context.ModuleTasks.FindAsync(taskId);

            Assert.NotNull(updatedTask);
            Assert.Equal("Updated Task", updatedTask.Title);
        }

        [Fact]
        public async Task DeleteTaskOfModuleAsync_ShouldDeleteTask()
        {
            var context = GetDbContext();
            var repository = new ModuleTaskRepository(context);
            var taskId = Guid.NewGuid();
            var moduleTask = new ModuleTask
            {
                Id = taskId,
                ModuleID = Guid.NewGuid(),
                Title = "Task 1",
                Description = "Task description",
                Type = "Assignment",
                DateCreated = DateOnly.FromDateTime(DateTime.UtcNow),
                Deadline = DateTime.UtcNow.AddDays(7)
            };
            await context.ModuleTasks.AddAsync(moduleTask);
            await context.SaveChangesAsync();

            await repository.DeleteTaskOfModuleAsync(taskId);
            var deletedTask = await context.ModuleTasks.FindAsync(taskId);

            Assert.Null(deletedTask);
        }
    }
}

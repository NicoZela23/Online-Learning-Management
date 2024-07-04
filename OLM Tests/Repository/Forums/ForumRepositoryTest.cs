using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities.Forums;
using Online_Learning_Management.Infrastructure.Data;
using Online_Learning_Management.Infrastructure.Repositories.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM_Tests.Repository.Forums
{
    public class ForumRepositoryTest
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }
        private Forum CreateTestForum()
        {
            return new Forum
            {
                Id = Guid.NewGuid(),
                CourseID = "3123123-3dc1 3d-d123-d13d-s",
                Title = "Test Forum",
                Description = "Test Description",
                
            };
        }

        [Fact]
        public async Task AddForumAsync_ShouldAddForum()
        {
            var context = GetDbContext();
            var repository = new ForumRepository(context);
            var forum = CreateTestForum();

            await repository.AddForumAsync(forum);

            var result = await context.Forums.FindAsync(forum.Id);
            Assert.NotNull(result);
            Assert.Equal(forum.Title, result.Title);
        }

        [Fact]
        public async Task GetForumByIdAsync_ShouldReturnForum()
        {
            var context = GetDbContext();
            var forum = CreateTestForum();
            await context.Forums.AddAsync(forum);
            await context.SaveChangesAsync();

            var repository = new ForumRepository(context);
            var result = await repository.GetForumByIdAsync(forum.Id);

            Assert.NotNull(result);
            Assert.Equal(forum.Title, result.Title);
        }

        [Fact]
        public async Task GetAllForumsAsync_ShouldReturnAllForums()
        {
            var context = GetDbContext();
            var forum1 = CreateTestForum();
            var forum2 = CreateTestForum();
            await context.Forums.AddRangeAsync(forum1, forum2);
            await context.SaveChangesAsync();

            var repository = new ForumRepository(context);
            var result = await repository.GetAllForumsAsync();

            Assert.Equal(2, result.Count()); 
        }

        [Fact]
        public async Task UpdateForumAsync_ShouldUpdateForum()
        {
            var context = GetDbContext();
            var forum = CreateTestForum();
            await context.Forums.AddAsync(forum);
            await context.SaveChangesAsync();

            var repository = new ForumRepository(context);
            forum.Title = "Updated Title";
            await repository.UpdateForumAsync(forum);

            var updatedForum = await context.Forums.FindAsync(forum.Id);
            Assert.NotNull(updatedForum);
            Assert.Equal("Updated Title", updatedForum.Title);
        }

        [Fact]
        public async Task DeleteForumAsync_ShouldRemoveForum()
        {
            var context = GetDbContext();
            var forum = CreateTestForum();
            await context.Forums.AddAsync(forum);
            await context.SaveChangesAsync();

            var repository = new ForumRepository(context);
            await repository.DeleteForumAsync(forum.Id);

            var result = await context.Forums.FindAsync(forum.Id);
            Assert.Null(result);
        }
    }
}

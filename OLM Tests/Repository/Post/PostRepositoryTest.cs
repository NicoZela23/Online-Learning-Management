using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Online_Learning_Management.Domain.Entities.Post;
using Online_Learning_Management.Infrastructure.Data;
using Online_Learning_Management.Infrastructure.Repositories.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM_Tests.Repository.Post
{
    public class PostRepositoryTest
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task AddPostAsync_ShouldAddPost()
        {
            var context = GetDbContext();
            var repository = new PostRepository(context);
            var post = new Posts
            {
                Id = Guid.NewGuid(),
                ForumId = Guid.NewGuid(),
                Content = "Post content",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            await repository.AddPostAsync(post);

            var result = await context.Posts.FindAsync(post.Id);
            Assert.NotNull(result);
            Assert.Equal(post.ForumId, result.ForumId);
            Assert.Equal(post.Content, result.Content);
            Assert.Equal(post.CreatedAt, result.CreatedAt);
        }

        [Fact]
        public async Task DeletePostAsync_ShouldDeletePost()
        {
            var context = GetDbContext();
            var repository = new PostRepository(context);
            var postId = Guid.NewGuid();
            var post = new Posts { Id = postId, Content = "Test Post" };
            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();

            await repository.DeletePostAsync(postId);

            var deletedPost = await context.Posts.FindAsync(postId);
            Assert.Null(deletedPost);
        }

        [Fact]
        public async Task GetAllPostsAsync_ShouldReturnAllPosts()
        {

            var context = GetDbContext();
            var repository = new PostRepository(context);
            var posts = new List<Posts>
            {
                new Posts { Id = Guid.NewGuid(), Content = "Post 1" },
                new Posts { Id = Guid.NewGuid(), Content = "Post 2" },
                new Posts { Id = Guid.NewGuid(), Content = "Post 3" }
            };
            await context.Posts.AddRangeAsync(posts);
            await context.SaveChangesAsync();

            var result = await repository.GetAllPostsAsync();

            Assert.Equal(posts.Count, result.Count());
            foreach (var post in posts)
            {
                Assert.Contains(result, p => p.Content == post.Content);
            }
        }

        [Fact]
        public async Task GetPostByIdAsync_ShouldReturnPostById()
        {
            var context = GetDbContext();
            var repository = new PostRepository(context);
            var postId = Guid.NewGuid();
            var post = new Posts { Id = postId, Content = "Test Post" };
            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();

            var result = await repository.GetPostByIdAsync(postId);

            Assert.NotNull(result);
            Assert.Equal(postId, result.Id);
            Assert.Equal(post.Content, result.Content);
        }

        [Fact]
        public async Task UpdatePostAsync_ShouldUpdatePost()
        {
            var context = GetDbContext();
            var repository = new PostRepository(context);
            var postId = Guid.NewGuid();
            var originalPost = new Posts { Id = postId, Content = "Original Post" };
            await context.Posts.AddAsync(originalPost);
            await context.SaveChangesAsync();

            context.Entry(originalPost).State = EntityState.Detached;

            var updatedPost = new Posts { Id = postId, Content = "Updated Post" };

            await repository.UpdatePostAsync(updatedPost);

            var result = await context.Posts.FindAsync(postId);
            Assert.NotNull(result);
            Assert.Equal(updatedPost.Content, result.Content);
        }

    }
}

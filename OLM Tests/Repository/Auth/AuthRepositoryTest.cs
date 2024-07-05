using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities.Auth;
using Online_Learning_Management.Infrastructure.Data;
using Online_Learning_Management.Infrastructure.Repositories.Auth;

namespace OLM_Tests.Repository.Auth
{
    public class AuthRepositoryTest
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var dbContext = new ApplicationDbContext(options);
            return dbContext;
        }

        private User CreateTestUser()
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Username = "testuser",
                Password = "password",
                CreatedAt = DateTime.UtcNow,
                EmailAddress = "testuser@example.com",
                LastName = "User",
                Name = "Test",
                Role = "Student"
            };
        }

        [Fact]
        public async Task AddUserAsync_ShouldAddUser()
        {
            var context = GetDbContext();
            var repository = new UserAuthRepository(context);
            var user = CreateTestUser();

            var result = await repository.AddUserAsync(user);

            Assert.NotNull(result);
            Assert.Equal("testuser", result.Username);
            Assert.Single(context.Users);
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldRemoveUser()
        {
            var context = GetDbContext();
            var user = CreateTestUser();
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            var repository = new UserAuthRepository(context);
            await repository.DeleteUserAsync(user.Id);

            Assert.Empty(context.Users);
        }

        [Fact]
        public async Task GetAllUsersAsync_ShouldReturnAllUsers()
        {
            var context = GetDbContext();
            var users = new List<User>
            {
                CreateTestUser(),
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "user2",
                    Password = "password2",
                    CreatedAt = DateTime.UtcNow,
                    EmailAddress = "user2@example.com",
                    LastName = "User2",
                    Name = "Test2",
                    Role = "Instructor"
                }
            };
            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();

            var repository = new UserAuthRepository(context);
            var result = await repository.GetAllUsersAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUser()
        {
            var context = GetDbContext();
            var user = CreateTestUser();
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            var repository = new UserAuthRepository(context);
            var result = await repository.GetUserByIdAsync(user.Id);

            Assert.NotNull(result);
            Assert.Equal("testuser", result.Username);
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldUpdateUser()
        {
            var context = GetDbContext();
            var user = CreateTestUser();
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            user.Password = "newpassword";
            var repository = new UserAuthRepository(context);
            var result = await repository.UpdateUserAsync(user);

            Assert.NotNull(result);
            Assert.Equal("newpassword", result.Password);
        }

        [Fact]
        public async Task GetUserByLoginCredentials_ShouldReturnUser()
        {
            var context = GetDbContext();
            var user = CreateTestUser();
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            var repository = new UserAuthRepository(context);
            var login = new UserLogin { Username = "testuser", Password = "password" };
            var result = await repository.GetUserByLoginCredentials(login);

            Assert.NotNull(result);
            Assert.Equal("testuser", result.Username);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Moq;
using Online_Learning_Management.Application.Auth.Responses;
using Online_Learning_Management.Domain.Entities.Auth;
using Online_Learning_Management.Domain.Exceptions.Auth;
using Online_Learning_Management.Domain.Interfaces.Auth;
using Online_Learning_Management.Infrastructure.DTOs.Auth;
using Online_Learning_Management.Presentation.Controllers;

namespace OLM_Tests.Users
{
    public class UsersTests
    {
        private readonly Mock<IAuthUserService> _userService;
        private readonly UserController _userController;

        public UsersTests()
        {
            _userService = new Mock<IAuthUserService>();
            _userController = new UserController(_userService.Object);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsOkResult_WithListOfUsers()
        {
            var users = new List<User> { new User { Id = Guid.NewGuid(), Username = "TestUser" } };
            _userService.Setup(service => service.GetAllUsersAsync()).ReturnsAsync(users);

            var result = await _userController.GetAllUsers();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<User>>(okResult.Value);
            Assert.Equal(users, returnValue);
        }

        [Fact]
        public async Task GetUserById_ReturnsOkResult_WithUser()
        {
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Username = "TestUser" };
            _userService.Setup(service => service.GetUserByIdAsync(userId)).ReturnsAsync(user);

            var result = await _userController.GetUserById(userId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<User>(okResult.Value);
            Assert.Equal(user, returnValue);
        }

        [Fact]
        public async Task GetUserById_ReturnsBadRequest_WhenUserNotFound()
        {
            var userId = Guid.NewGuid();
            _userService.Setup(service => service.GetUserByIdAsync(userId)).ThrowsAsync(new UserNotFoundException());

            var result = await _userController.GetUserById(userId);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("User not found", badRequestResult.Value);
        }

        [Fact]
        public async Task AddUser_ReturnsOkResult_WithApiResponse()
        {
            var createUserDto = new CreateUserDTO { Username = "NewUser" };
            var user = new User { Id = Guid.NewGuid(), Username = "NewUser" };
            _userService.Setup(service => service.AddUserAsync(createUserDto)).ReturnsAsync(user);

            var result = await _userController.AddUser(createUserDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ApiResponse>(okResult.Value);
            Assert.Equal("User created successfully", returnValue.Message);
            Assert.Equal(user, returnValue.Data);
        }

        [Fact]
        public async Task UpdateUser_ReturnsOkResult_WithApiResponse()
        {
            var userId = Guid.NewGuid();
            var updateUserDto = new UpdateUserDTO { Username = "UpdatedUser" };
            var user = new User { Id = userId, Username = "UpdatedUser" };
            _userService.Setup(service => service.UpdateUserAsync(userId, updateUserDto)).ReturnsAsync(user);

            var result = await _userController.UpdateUser(userId, updateUserDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ApiResponse>(okResult.Value);
            Assert.Equal("User updated successfully", returnValue.Message);
            Assert.Equal(user, returnValue.Data);
        }

        [Fact]
        public async Task UpdateUser_ReturnsNotFound_WhenUserNotFound()
        {
            var userId = Guid.NewGuid();
            var updateUserDto = new UpdateUserDTO { Username = "UpdatedUser" };
            _userService.Setup(service => service.UpdateUserAsync(userId, updateUserDto)).ThrowsAsync(new UserNotFoundException());

            var result = await _userController.UpdateUser(userId, updateUserDto);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found", notFoundResult.Value);
        }

        [Fact]
        public async Task DeleteUser_ReturnsNoContent()
        {
            var userId = Guid.NewGuid();
            _userService.Setup(service => service.DeleteUserAsync(userId)).Returns(Task.CompletedTask);

            var result = await _userController.DeleteUser(userId);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ReturnsNotFound_WhenUserNotFound()
        {
            var userId = Guid.NewGuid();
            _userService.Setup(service => service.DeleteUserAsync(userId)).ThrowsAsync(new UserNotFoundException());

            var result = await _userController.DeleteUser(userId);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found", notFoundResult.Value);
        }
    }


}

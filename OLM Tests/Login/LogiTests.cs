using Microsoft.AspNetCore.Mvc;
using Moq;
using Online_Learning_Management.Domain.Entities.Auth;
using Online_Learning_Management.Domain.Exceptions.Auth;
using Online_Learning_Management.Domain.Interfaces.Auth;
using Online_Learning_Management.Presentation.Controllers;

namespace OLM_Tests.Login
{
    public class LogiTests
    {
        private readonly Mock<IAuthValidService> _authValidService;
        private readonly LoginController _loginController;

        public LogiTests()
        {
            _authValidService = new Mock<IAuthValidService>();
            _loginController = new LoginController(_authValidService.Object);
        }

        [Fact]
        public async Task Login_ReturnsOkResult_WithToken()
        {
            var userLogin = new UserLogin { Username = "testuser", Password = "password" };
            var user = new User { Id = Guid.NewGuid(), Username = "testuser" };
            var token = "test-token";

            _authValidService.Setup(service => service.Authenticate(userLogin)).ReturnsAsync(user);
            _authValidService.Setup(service => service.Generate(user)).Returns(token);

            var result = await _loginController.Login(userLogin);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<string>(okResult.Value);
            Assert.Equal(token, returnValue);
        }

        [Fact]
        public async Task Login_ReturnsNotFound_WhenUserNotFound()
        {
            var userLogin = new UserLogin { Username = "testuser", Password = "password" };
            _authValidService.Setup(service => service.Authenticate(userLogin)).ThrowsAsync(new UserNotFoundException());

            var result = await _loginController.Login(userLogin);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found", notFoundResult.Value);
        }

        [Fact]
        public async Task Login_ReturnsBadRequest_OnException()
        {
            var userLogin = new UserLogin { Username = "testuser", Password = "password" };
            _authValidService.Setup(service => service.Authenticate(userLogin)).ThrowsAsync(new Exception("Some error"));

            var result = await _loginController.Login(userLogin);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Some error", badRequestResult.Value);
        }
    }
}

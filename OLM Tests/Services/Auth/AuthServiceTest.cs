using Castle.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Moq;
using Online_Learning_Management.Application.Auth.Services;
using Online_Learning_Management.Domain.Entities.Auth;
using Online_Learning_Management.Domain.Exceptions.Auth;
using Online_Learning_Management.Domain.Interfaces.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM_Tests.Services.Auth
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserAuthRepository> _mockUserAuthRepository;
        private readonly Mock<Microsoft.Extensions.Configuration.IConfiguration> _mockConfiguration;

        public AuthServiceTests()
        {
            _mockUserAuthRepository = new Mock<IUserAuthRepository>();
            _mockConfiguration = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
        }

        [Fact]
        public async void Authenticate_ValidUser_ReturnsUser()
        {
            var authService = new AuthService(_mockUserAuthRepository.Object, _mockConfiguration.Object);
            var userLogin = new UserLogin { Username = "testuser", Password = "password" };
            var expectedUser = new User { Username = "testuser", EmailAddress = "test@example.com", Role = "User" };

            _mockUserAuthRepository.Setup(repo => repo.GetUserByLoginCredentials(userLogin))
                                   .ReturnsAsync(expectedUser);

            var result = await authService.Authenticate(userLogin);

            Assert.Equal(expectedUser, result);
        }

        [Fact]
        public async void Authenticate_InvalidUser_ThrowsUserNotFoundException()
        {
            var authService = new AuthService(_mockUserAuthRepository.Object, _mockConfiguration.Object);
            var userLogin = new UserLogin { Username = "testuser", Password = "password" };

            _mockUserAuthRepository.Setup(repo => repo.GetUserByLoginCredentials(userLogin))
                                   .ReturnsAsync((User)null);

            await Assert.ThrowsAsync<UserNotFoundException>(() => authService.Authenticate(userLogin));
        }
    }
}

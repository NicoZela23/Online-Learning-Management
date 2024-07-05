using Online_Learning_Management.Application.Auth.Validator;
using Online_Learning_Management.Infrastructure.DTOs.Auth;

namespace OLM_Tests.Validator.Auth
{
    public class AuthValidatorTest
    {
        private readonly CreateUserValidator _validator;

        public AuthValidatorTest()
        {
            _validator = new CreateUserValidator();
        }

        [Fact]
        public void All_Fields_Validation()
        {
            var dto = new CreateUserDTO
            {
                Username = null,
                Password = null,
                EmailAddress = null,
                Role = null
            };

            var result = _validator.Validate(dto);

            Assert.False(result.IsValid);

            var usernameError = result.Errors.FirstOrDefault(e => e.PropertyName == nameof(dto.Username));
            Assert.NotNull(usernameError);
            Assert.Equal("Username is required", usernameError.ErrorMessage);

            var passwordError = result.Errors.FirstOrDefault(e => e.PropertyName == nameof(dto.Password));
            Assert.NotNull(passwordError);
            Assert.Equal("'Password' must not be empty.", passwordError.ErrorMessage);

            var emailError = result.Errors.FirstOrDefault(e => e.PropertyName == nameof(dto.EmailAddress));
            Assert.NotNull(emailError);
            Assert.Equal("Email is required", emailError.ErrorMessage);

            var roleError = result.Errors.FirstOrDefault(e => e.PropertyName == nameof(dto.Role));
            Assert.NotNull(roleError);
            Assert.Equal("Role is required", roleError.ErrorMessage);
        }
    }
}

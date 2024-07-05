using Online_Learning_Management.Application.Auth.Validator;
using Online_Learning_Management.Infrastructure.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM_Tests.Validator.Auth
{
    public class UpdateUserValidatorTests
    {
        private readonly UpdateUserValidator _validator;

        public UpdateUserValidatorTests()
        {
            _validator = new UpdateUserValidator();
        }

        [Fact]
        public void All_Fields_Validation()
        {
            var dto = new UpdateUserDTO
            {
                Username = null,
                Name = null,
                LastName = null,
                Password = null,
                EmailAddress = null
            };

            var result = _validator.Validate(dto);

            Assert.False(result.IsValid);

            var usernameError = result.Errors.FirstOrDefault(e => e.PropertyName == nameof(dto.Username));
            Assert.NotNull(usernameError);
            Assert.Equal("Username is required", usernameError.ErrorMessage);

            var nameError = result.Errors.FirstOrDefault(e => e.PropertyName == nameof(dto.Name));
            Assert.NotNull(nameError);
            Assert.Equal("Name is required", nameError.ErrorMessage);

            var lastNameError = result.Errors.FirstOrDefault(e => e.PropertyName == nameof(dto.LastName));
            Assert.NotNull(lastNameError);
            Assert.Equal("Last name is required", lastNameError.ErrorMessage);

            var passwordError = result.Errors.FirstOrDefault(e => e.PropertyName == nameof(dto.Password));
            Assert.NotNull(passwordError);
            Assert.Equal("'Password' must not be empty.", passwordError.ErrorMessage);

            var emailError = result.Errors.FirstOrDefault(e => e.PropertyName == nameof(dto.EmailAddress));
            Assert.NotNull(emailError);
            Assert.Equal("Email is required", emailError.ErrorMessage);
        }
    }
}
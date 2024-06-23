using FluentValidation;
using Online_Learning_Management.Infrastructure.DTOs.Auth;

namespace Online_Learning_Management.Application.Auth.Validator
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserValidator() 
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Password is required and must be greater than 6 characters");
            RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("Email is required");
        }
    }
}

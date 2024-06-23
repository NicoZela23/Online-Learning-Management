using FluentValidation;
using Online_Learning_Management.Infrastructure.DTOs.Auth;

namespace Online_Learning_Management.Application.Auth.Validator
{
    public class CreateUserValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserValidator() 
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Password is required and must be greater than 6 characters");
            RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Role)
           .NotEmpty().WithMessage("Role is required")
           .Must(role => role == "Student" || role == "Instructor" || role == "Admin")
           .WithMessage("Invalid role. Must be Student, Instructor, or Admin.");
        }
    }
}

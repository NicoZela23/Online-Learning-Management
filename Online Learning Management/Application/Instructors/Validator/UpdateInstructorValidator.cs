using FluentValidation;
using Online_Learning_Management.Infrastructure.DTOs.Instructor;

namespace Online_Learning_Management.Application.Instructors.Validator
{
    public class UpdateInstructorValidator : AbstractValidator<UpdateInstructorDTO>
    {
        public UpdateInstructorValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("First Name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email is not valid");
        }
    }
}

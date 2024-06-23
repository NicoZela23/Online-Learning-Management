using FluentValidation;

using Online_Learning_Management.Infrastructure.DTOs.Student;

namespace Online_Learning_Management.Application.Students.Validator
{
    public class CreateStudentValidator : AbstractValidator<CreateStudentDTO>
    {
        public CreateStudentValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User Id is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("First Name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
        }
    }
}

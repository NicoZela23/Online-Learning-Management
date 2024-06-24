using FluentValidation;
using Online_Learning_Management.Infrastructure.DTOs.TaskStudent;

namespace Online_Learning_Management.Application.TaskStudent.Validator
{
    public class CreateTaskStudentValidator : AbstractValidator<CreateTaskStudentDTO>
    {
        public CreateTaskStudentValidator()
        {
            RuleFor(x => x.ModuleTaskID).NotEmpty()
               .WithMessage("ModuleTaskID is required.");
            RuleFor(x => x.StudentID).NotEmpty()
                .WithMessage("StudentID is required.");
        }
    }
}

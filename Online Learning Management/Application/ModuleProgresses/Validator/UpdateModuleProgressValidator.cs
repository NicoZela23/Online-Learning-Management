using FluentValidation;
using Online_Learning_Management.Infrastructure.DTOs.ModuleProgresses;

namespace Online_Learning_Management.Application.ModuleProgresses.Validator
{
    public class UpdateModuleProgressValidator : AbstractValidator<UpdateModuleProgressDTO>
    {
        public UpdateModuleProgressValidator()
        {
            RuleFor(x => x.CourseId).NotEmpty().WithMessage("CourseId is required.");
            RuleFor(x => x.ModuleId).NotEmpty().WithMessage("ModuleId is required.");
            RuleFor(x => x.StudentId).NotEmpty().WithMessage("StudentId is required.");
            RuleFor(x => x.Progress).NotEmpty().WithMessage("Progress is required.")
                .Must(x => x == "Approved" || x == "Reproved" || x == "InProgress").WithMessage("Progress must be 'Approved', 'Reproved' or 'InProgress'.");
        }
    }
}
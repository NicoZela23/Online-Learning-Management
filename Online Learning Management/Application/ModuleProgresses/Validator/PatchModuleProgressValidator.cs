using FluentValidation;
using Online_Learning_Management.Infrastructure.DTOs.ModuleProgresses;

namespace Online_Learning_Management.Application.ModuleProgresses.Validator
{
    public class PatchModuleProgressValidator : AbstractValidator<PatchModuleProgressDTO>
    {
        public PatchModuleProgressValidator()
        {
            RuleFor(x => x.Progress).NotEmpty().WithMessage("Progress is required and it must be :")
                .Must(x => x == "Approved" || x == "Reproved" || x == "InProgress").WithMessage(" 'Approved', 'Reproved' or 'InProgress'.");
        }
    }
}
using FluentValidation;
using Online_Learning_Management.Infrastructure.DTOs.ModuleTask;

namespace Online_Learning_Management.Application.ModuleTasks.Validator
{
    public class UpdateModuleTaskValidator : AbstractValidator<UpdateModuleTaskDTO>
    {
        public UpdateModuleTaskValidator() 
        {
            RuleFor(x => x.Title).NotEmpty()
                .WithMessage("Title is required.");
            RuleFor(x => x.Description).NotEmpty()
                .WithMessage("Description is required.");
            RuleFor(x => x.Type).NotEmpty()
                .WithMessage("Type is required.");
            RuleFor(x => x.Deadline).NotEmpty()
                .WithMessage("Deadline is required.")
               .GreaterThan(DateTime.UtcNow).WithMessage("Deadline must be greater than the current date and time.");
        }
    }
}

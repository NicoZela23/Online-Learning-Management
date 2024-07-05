using FluentValidation;
using Online_Learning_Management.Infrastructure.DTOs.Module;
namespace Online_Learning_Management.Application.Modules.Validator
{
    public class CreateModuleValidator : AbstractValidator<CreateModuleDTO>
    {
        public CreateModuleValidator()
        {
            RuleFor(x => x.CourseID).NotEmpty().WithMessage("CourseID is required.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Duration).GreaterThan(TimeSpan.Zero).WithMessage("Duration must be greater than zero.");
            RuleFor(x => x.LearningOutcomes).NotEmpty().WithMessage("LearningOutcomes is required.")
                .Must(lo => lo != null && lo.All(item => !string.IsNullOrEmpty(item)))
                .WithMessage("All LearningOutcomes must be non-empty strings.");
            RuleFor(x => x.Prerequisites).NotEmpty().WithMessage("Prerequisites is required.")
                .Must(p => p != null && p.All(item => !string.IsNullOrEmpty(item)))
                .WithMessage("All Prerequisites must be non-empty strings.");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("StartDate is required.")
                .LessThan(x => x.EndDate).WithMessage("StartDate must be less than EndDate.");
            RuleFor(x => x.EndDate).NotEmpty().WithMessage("EndDate is required.")
                .GreaterThan(x => x.StartDate).WithMessage("EndDate must be greater than StartDate.");
            RuleFor(x => x.Resources).NotEmpty().WithMessage("Resources is required.")
                .Must(r => r != null && r.All(item => !string.IsNullOrEmpty(item)))
                .WithMessage("All Resources must be non-empty strings.");
            RuleFor(x => x.AssessmentMethods).NotEmpty().WithMessage("AssessmentMethods are required")
                .Must(BeValidAssessment).WithMessage("Total must be 100");
        }

        private bool BeValidAssessment(Dictionary<string, int> assessmentMethods)
        {
            return assessmentMethods != null && assessmentMethods.Values.Sum() == 100;
        }
    }
}
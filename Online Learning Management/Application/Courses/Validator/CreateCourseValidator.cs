using FluentValidation;
using Online_Learning_Management.Infrastructure.DTOs;

namespace OnlineLearningManagement.Application.Courses.Validator
{
    public class CreateCourseValidator : AbstractValidator<CreateCourseDTO>
    {
        public CreateCourseValidator()
        {
            RuleFor(course => course.Title)
                .NotEmpty().WithMessage("The title of the course is required.")
                .MaximumLength(100).WithMessage("The title of the course cannot be more than 100 characters.")
                .Must(title => !string.IsNullOrEmpty(title)).WithMessage("The title of the course must be a non-empty string.");

            RuleFor(course => course.Description)
                .NotEmpty().WithMessage("The description of the course is required.")
                .MaximumLength(1000).WithMessage("The description of the course cannot be more than 1000 characters.")
                .Must(description => !string.IsNullOrEmpty(description)).WithMessage("The description of the course must be a non-empty string.");

            RuleFor(course => course.IdInstructor)
                .NotEmpty().WithMessage("The instructor of the course is required.");

            RuleFor(course => course.Content)
                .NotEmpty().WithMessage("The content of the course is required.")
                .Must(content => content != null && content.Count > 0).WithMessage("The content of the course must be a non-empty list.");
            
            RuleFor(course => course.DurationInWeeks)
                .NotEmpty().WithMessage("The duration of the course is required.")
                .InclusiveBetween(1, 10).WithMessage("DurationInWeeks must be greater than 0 or less or equal to 10");

        }
    }
}
using FluentValidation;
using Online_Learning_Management.Infrastructure.DTOs.File;

namespace Online_Learning_Management.Application.Files.Validator
{
    public class CreateFileValidator : AbstractValidator<CreateFileDTO>
    {
        public CreateFileValidator()
        {
            RuleFor(x => x.FileName).NotEmpty().WithMessage("FileName is required.");
            RuleFor(x => x.BlobURL).NotEmpty().WithMessage("BlobURL is required.");
            RuleFor(x => x.FileSize).GreaterThan(0).WithMessage("FileSize must be greater than zero.");
            RuleFor(x => x.FileSize).LessThan(20).WithMessage("FileSize must be less than 20.");
            RuleFor(x => x.UploadedAt).NotEmpty().WithMessage("UploadedAt is required.");
        }
    }
}

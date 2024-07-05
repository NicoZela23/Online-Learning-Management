using Online_Learning_Management.Application.Files.Validator;
using Online_Learning_Management.Infrastructure.DTOs.File;

namespace OLM_Tests.Validator.Files
{
    public class CreateFileValidatorTests
    {
        private readonly CreateFileValidator _validator;

        public CreateFileValidatorTests()
        {
            _validator = new CreateFileValidator();
        }

        [Fact]
        public void All_Fields_Validation()
        {
            var dto = new CreateFileDTO
            {
                FileName = null,
                BlobURL = null,
                FileSize = 0,
                UploadedAt = DateTime.UtcNow
            };

            var result = _validator.Validate(dto);

            Assert.False(result.IsValid);

            var fileNameError = result.Errors.FirstOrDefault(e => e.PropertyName == nameof(dto.FileName));
            Assert.NotNull(fileNameError);
            Assert.Equal("FileName is required.", fileNameError.ErrorMessage);

            var blobURLError = result.Errors.FirstOrDefault(e => e.PropertyName == nameof(dto.BlobURL));
            Assert.NotNull(blobURLError);
            Assert.Equal("BlobURL is required.", blobURLError.ErrorMessage);

            var fileSizeError = result.Errors.FirstOrDefault(e => e.PropertyName == nameof(dto.FileSize));
            Assert.NotNull(fileSizeError);
            Assert.Equal("FileSize must be greater than zero.", fileSizeError.ErrorMessage);
        }
    }
}
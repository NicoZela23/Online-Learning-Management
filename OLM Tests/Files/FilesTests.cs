using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Online_Learning_Management.Application.Files.Responses;
using Online_Learning_Management.Domain.Entities.Files;
using Online_Learning_Management.Domain.Interfaces.Files;
using Online_Learning_Management.Presentation.Controllers;

namespace OLM_Tests.Files
{
    public class FilesTests
    {
        private readonly Mock<IFileService> _fileService;
        private readonly FilesController _fileController;

        public FilesTests()
        {
            _fileService = new Mock<IFileService>();
            _fileController = new FilesController(_fileService.Object);
        }

        [Fact]
        public async Task GetAllFilesData_ReturnsOkResult_WithListOfFileMetadata()
        {
            var files = new List<FileMetadata> { new FileMetadata { Id = Guid.NewGuid(), FileName = "TestFile" } };
            _fileService.Setup(service => service.GetAllFileDataAsync()).ReturnsAsync(files);

            var result = await _fileController.GetAllFilesData();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<FileMetadata>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<List<FileMetadata>>(okResult.Value);
            Assert.Equal(files, returnValue);
        }

        [Fact]
        public async Task GetAllFilesData_ReturnsBadRequest_OnException()
        {
            _fileService.Setup(service => service.GetAllFileDataAsync()).ThrowsAsync(new Exception("Some error"));

            var result = await _fileController.GetAllFilesData();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<FileMetadata>>>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            Assert.Equal("Some error", badRequestResult.Value);
        }

        [Fact]
        public async Task GetFileDataById_ReturnsOkResult_WithFileMetadata()
        {
            var fileId = Guid.NewGuid();
            var fileMetadata = new FileMetadata { Id = fileId, FileName = "TestFile" };
            _fileService.Setup(service => service.GetFileDataByIdAsync(fileId)).ReturnsAsync(fileMetadata);

            var result = await _fileController.GetFileDataById(fileId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<FileMetadata>(okResult.Value);
            Assert.Equal(fileMetadata, returnValue);
        }

        [Fact]
        public async Task GetFileDataById_ReturnsNotFound_WhenFileNotFound()
        {
            var fileId = Guid.NewGuid();
            _fileService.Setup(service => service.GetFileDataByIdAsync(fileId)).ThrowsAsync(new FileNotFoundException("File not found"));

            var result = await _fileController.GetFileDataById(fileId);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("File not found", notFoundResult.Value);
        }

        [Fact]
        public async Task GetFileDataById_ReturnsBadRequest_OnException()
        {
            var fileId = Guid.NewGuid();
            _fileService.Setup(service => service.GetFileDataByIdAsync(fileId)).ThrowsAsync(new Exception("Some error"));

            var result = await _fileController.GetFileDataById(fileId);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Some error", badRequestResult.Value);
        }

        [Fact]
        public async Task DeleteFileMetadata_ReturnsNoContent_OnSuccess()
        {
            var fileId = Guid.NewGuid();
            _fileService.Setup(service => service.DeleteFileDataAsync(fileId)).Returns(Task.CompletedTask);

            var result = await _fileController.DeleteFileMetadata(fileId);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteFileMetadata_ReturnsNotFound_WhenFileNotFound()
        {
            var fileId = Guid.NewGuid();
            _fileService.Setup(service => service.DeleteFileDataAsync(fileId)).ThrowsAsync(new ArgumentException("File not found"));

            var result = await _fileController.DeleteFileMetadata(fileId);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("File not found.", notFoundResult.Value);
        }

        [Fact]
        public async Task UploadFile_ReturnsOkResult_WithFileMetadata()
        {
            var file = new Mock<IFormFile>();
            var fileMetadata = new FileMetadata { Id = Guid.NewGuid(), FileName = "TestFile" };
            var response = new ApiResponce("File created successfully", fileMetadata);

            file.Setup(f => f.FileName).Returns("TestFile.txt");
            file.Setup(f => f.Length).Returns(1);
            file.Setup(f => f.OpenReadStream()).Returns(new MemoryStream());

            _fileService.Setup(service => service.UploadAndAddFileAsync(file.Object)).ReturnsAsync(fileMetadata);

            var result = await _fileController.UploadFile(file.Object);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ApiResponce>(okResult.Value);
            Assert.Equal(response.Message, returnValue.Message);
            Assert.Equal(fileMetadata, returnValue.Data);
        }

        [Fact]
        public async Task UploadFile_ReturnsBadRequest_OnException()
        {
            var file = new Mock<IFormFile>();
            file.Setup(f => f.FileName).Returns("TestFile.txt");
            file.Setup(f => f.Length).Returns(1);
            file.Setup(f => f.OpenReadStream()).Returns(new MemoryStream());

            _fileService.Setup(service => service.UploadAndAddFileAsync(file.Object)).ThrowsAsync(new Exception("Some error"));

            var result = await _fileController.UploadFile(file.Object);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Some error", badRequestResult.Value);
        }
    }
}

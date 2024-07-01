using Microsoft.AspNetCore.Mvc;
using Moq;
using Online_Learning_Management.Domain.Entities.Instructors;
using Online_Learning_Management.Domain.Exceptions.Auth;
using Online_Learning_Management.Domain.Interfaces.Instructors;
using Online_Learning_Management.Presentation.Controllers;

namespace OLM_Tests.Intructors
{
    public class InstructorsTests
    {
        private readonly Mock<INstructorService> _instructorService;
        private readonly InstructorsController _instructorsController;  

        public InstructorsTests()
        {
            _instructorService = new Mock<INstructorService>();
            _instructorsController = new InstructorsController(_instructorService.Object);
        }

        [Fact]
        public async Task GetAllInstructors_ReturnsOkResult_WithListOfInstructors()
        {
            var instructors = new List<Instructor> { new Instructor { Id = Guid.NewGuid(), Name = "Test Instructor" } };
            _instructorService.Setup(service => service.GetAllInstructorsAsync()).ReturnsAsync(instructors);

            var result = await _instructorsController.GetAllInstructors();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Instructor>>(okResult.Value);
            Assert.Equal(instructors, returnValue);
        }

        [Fact]
        public async Task GetAllInstructors_ReturnsBadRequest_OnException()
        {
            _instructorService.Setup(service => service.GetAllInstructorsAsync()).ThrowsAsync(new Exception("Some error"));

            var result = await _instructorsController.GetAllInstructors();

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Some error", badRequestResult.Value);
        }

        [Fact]
        public async Task GetInstructorById_ReturnsOkResult_WithInstructor()
        {
            var instructorId = Guid.NewGuid();
            var instructor = new Instructor { Id = instructorId, Name = "Test Instructor" };
            _instructorService.Setup(service => service.GetInstructorByIdAsync(instructorId)).ReturnsAsync(instructor);

            var result = await _instructorsController.GetInstructorById(instructorId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Instructor>(okResult.Value);
            Assert.Equal(instructor, returnValue);
        }

        [Fact]
        public async Task GetInstructorById_ReturnsNotFound_WhenInstructorNotFound()
        {
            var instructorId = Guid.NewGuid();
            _instructorService.Setup(service => service.GetInstructorByIdAsync(instructorId)).ThrowsAsync(new InstructorNotFoundException());

            var result = await _instructorsController.GetInstructorById(instructorId);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Instructor not found", notFoundResult.Value);
        }

        [Fact]
        public async Task GetInstructorById_ReturnsBadRequest_OnException()
        {
            var instructorId = Guid.NewGuid();
            _instructorService.Setup(service => service.GetInstructorByIdAsync(instructorId)).ThrowsAsync(new Exception("Some error"));

            var result = await _instructorsController.GetInstructorById(instructorId);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Some error", badRequestResult.Value);
        }
    }
}

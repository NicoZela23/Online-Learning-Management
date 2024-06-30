using Microsoft.AspNetCore.Mvc;
using Moq;
using Online_Learning_Management.Domain.Entities.Students;
using Online_Learning_Management.Domain.Exceptions.Auth;
using Online_Learning_Management.Domain.Interfaces.Students;
using Online_Learning_Management.Presentation.Controllers;

namespace OLM_Tests.Students
{
    public class StudentsTests
    {
        private readonly Mock<IStudentServices> _studentService;
        private readonly StudentsController _studentsController;

        public StudentsTests()
        {
            _studentService = new Mock<IStudentServices>();
            _studentsController = new StudentsController(_studentService.Object);
        }

        [Fact]
        public async Task GetStudentById_ReturnsOkResult_WithStudent()
        {
            var studentId = Guid.NewGuid();
            var student = new Student { Id = studentId, Name = "Test Student" };
            _studentService.Setup(service => service.GetStudentByIdAsync(studentId)).ReturnsAsync(student);

            var result = await _studentsController.GetStudentById(studentId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Student>(okResult.Value);
            Assert.Equal(student, returnValue);
        }

        [Fact]
        public async Task GetStudentById_ReturnsNotFound_WhenStudentNotFound()
        {
            var studentId = Guid.NewGuid();
            _studentService.Setup(service => service.GetStudentByIdAsync(studentId)).ThrowsAsync(new StudentNotfoundException());

            var result = await _studentsController.GetStudentById(studentId);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Student not found", notFoundResult.Value);
        }

        [Fact]
        public async Task GetStudentById_ReturnsBadRequest_OnException()
        {
            var studentId = Guid.NewGuid();
            _studentService.Setup(service => service.GetStudentByIdAsync(studentId)).ThrowsAsync(new Exception("Some error"));

            var result = await _studentsController.GetStudentById(studentId);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Some error", badRequestResult.Value);
        }

        [Fact]
        public async Task GetAllStudents_ReturnsOkResult_WithListOfStudents()
        {
            var students = new List<Student> { new Student { Id = Guid.NewGuid(), Name = "Test Student" } };
            _studentService.Setup(service => service.GetAllStudentsAsync()).ReturnsAsync(students);

            var result = await _studentsController.GetAllStudents();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Student>>(okResult.Value);
            Assert.Equal(students, returnValue);
        }

        [Fact]
        public async Task GetAllStudents_ReturnsBadRequest_OnException()
        {
            _studentService.Setup(service => service.GetAllStudentsAsync()).ThrowsAsync(new Exception("Some error"));

            var result = await _studentsController.GetAllStudents();

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Some error", badRequestResult.Value);
        }
    }
}

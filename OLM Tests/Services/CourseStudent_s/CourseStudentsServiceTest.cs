using AutoMapper;
using Moq;
using Online_Learning_Management.Domain.Entities.CourseStudent;
using Online_Learning_Management.Domain.Interfaces.CourseStudents;
using Online_Learning_Management.Infrastructure.DTOs.CourseStudents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM_Tests.Services.CourseStudent_s
{
    public class CourseStudentsServiceTests
    {
        private readonly Mock<ICourseStudentsRepository> _mockCourseStudentsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CourseStudentsService _courseStudentsService;

        public CourseStudentsServiceTests()
        {
            _mockCourseStudentsRepository = new Mock<ICourseStudentsRepository>();
            _mockMapper = new Mock<IMapper>();
            _courseStudentsService = new CourseStudentsService(_mockCourseStudentsRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllCourseStudentsAsync_ReturnsAllCourseStudents()
        {
            // Arrange
            var courseStudents = new List<CourseStudent> { new CourseStudent(), new CourseStudent() };
            _mockCourseStudentsRepository.Setup(r => r.GetAllCourseStudentsAsync()).ReturnsAsync(courseStudents);
            _mockMapper.Setup(m => m.Map<IEnumerable<CourseStudentDTO>>(courseStudents)).Returns(courseStudents.Select(cs => new CourseStudentDTO()));

            // Act
            var result = await _courseStudentsService.GetAllCourseStudentsAsync();

            // Assert
            Assert.Equal(courseStudents.Count, result.Count());
        }

        [Fact]
        public async Task GetCourseStudentByIdAsync_ExistingId_ReturnsCourseStudent()
        {
            // Arrange
            var courseStudentId = Guid.NewGuid();
            var courseStudent = new CourseStudent { Id = courseStudentId };
            _mockCourseStudentsRepository.Setup(r => r.GetCourseStudentByIdAsync(courseStudentId)).ReturnsAsync(courseStudent);
            _mockMapper.Setup(m => m.Map<CourseStudentDTO>(courseStudent)).Returns(new CourseStudentDTO());

            // Act
            var result = await _courseStudentsService.GetCourseStudentByIdAsync(courseStudentId);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetCourseStudentByIdAsync_NonExistingId_ThrowsKeyNotFoundException()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();
            _mockCourseStudentsRepository.Setup(r => r.GetCourseStudentByIdAsync(nonExistingId)).ReturnsAsync((CourseStudent)null);

            // Act and Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _courseStudentsService.GetCourseStudentByIdAsync(nonExistingId));
        }

        [Fact]
        public async Task DeleteCourseStudentAsync_ExistingId_DeletesCourseStudent()
        {
            // Arrange
            var courseStudentId = Guid.NewGuid();
            var courseStudent = new CourseStudent { Id = courseStudentId };
            _mockCourseStudentsRepository.Setup(r => r.GetCourseStudentByIdAsync(courseStudentId)).ReturnsAsync(courseStudent);
            _mockCourseStudentsRepository.Setup(r => r.DeleteCourseStudentAsync(courseStudentId)).Returns(Task.CompletedTask);

            // Act
            await _courseStudentsService.DeleteCourseStudentAsync(courseStudentId);

            // Assert
            _mockCourseStudentsRepository.Verify(r => r.DeleteCourseStudentAsync(courseStudentId), Times.Once);
        }

        [Fact]
        public async Task DeleteCourseStudentAsync_NonExistingId_ThrowsKeyNotFoundException()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();
            _mockCourseStudentsRepository.Setup(r => r.GetCourseStudentByIdAsync(nonExistingId)).ReturnsAsync((CourseStudent)null);

            // Act and Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _courseStudentsService.DeleteCourseStudentAsync(nonExistingId));
        }

        [Fact]
        public async Task WithdrawCourseStudentAsync_ExistingStudentAndCourse_DeletesCourseStudent()
        {
            // Arrange
            var studentId = Guid.NewGuid();
            var courseId = Guid.NewGuid();
            var courseStudent = new CourseStudent { Id = Guid.NewGuid() };
            _mockCourseStudentsRepository.Setup(r => r.GetCourseStudentByStudentAndCourseAsync(studentId, courseId)).ReturnsAsync(courseStudent);
            _mockCourseStudentsRepository.Setup(r => r.DeleteCourseStudentAsync(courseStudent.Id)).Returns(Task.CompletedTask);

            // Act
            await _courseStudentsService.WithdrawCourseStudentAsync(studentId, courseId);

            // Assert
            _mockCourseStudentsRepository.Verify(r => r.DeleteCourseStudentAsync(courseStudent.Id), Times.Once);
        }

        [Fact]
        public async Task WithdrawCourseStudentAsync_NonExistingStudentOrCourse_ThrowsKeyNotFoundException()
        {
            // Arrange
            var nonExistingStudentId = Guid.NewGuid();
            var courseId = Guid.NewGuid();
            _mockCourseStudentsRepository.Setup(r => r.GetCourseStudentByStudentAndCourseAsync(nonExistingStudentId, courseId)).ReturnsAsync((CourseStudent)null);

            // Act and Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _courseStudentsService.WithdrawCourseStudentAsync(nonExistingStudentId, courseId));
        }

        [Fact]
        public async Task EnrollCourseStudentAsync_ValidEnrollment_EnrollsStudent()
        {
            // Arrange
            var enrollStudentDTO = new EnrollStudentDTO { CourseID = Guid.NewGuid(), StudentID = Guid.NewGuid() };
            _mockCourseStudentsRepository.Setup(r => r.CourseExistsAsync(enrollStudentDTO.CourseID)).ReturnsAsync(true);
            _mockCourseStudentsRepository.Setup(r => r.StudentExistsAsync(enrollStudentDTO.StudentID)).ReturnsAsync(true);
            _mockCourseStudentsRepository.Setup(r => r.GetCourseStudentByStudentAndCourseAsync(enrollStudentDTO.StudentID, enrollStudentDTO.CourseID)).ReturnsAsync((CourseStudent)null);
            _mockMapper.Setup(m => m.Map<CourseStudent>(enrollStudentDTO)).Returns(new CourseStudent());

            // Act
            await _courseStudentsService.EnrollCourseStudentAsync(enrollStudentDTO);

            // Assert
            _mockCourseStudentsRepository.Verify(r => r.AddCourseStudentAsync(It.IsAny<CourseStudent>()), Times.Once);
        }

        [Fact]
        public async Task EnrollCourseStudentAsync_NonExistingCourse_ThrowsKeyNotFoundException()
        {
            // Arrange
            var enrollStudentDTO = new EnrollStudentDTO { CourseID = Guid.NewGuid(), StudentID = Guid.NewGuid() };
            _mockCourseStudentsRepository.Setup(r => r.CourseExistsAsync(enrollStudentDTO.CourseID)).ReturnsAsync(false);

            // Act and Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _courseStudentsService.EnrollCourseStudentAsync(enrollStudentDTO));
        }

        [Fact]
        public async Task EnrollCourseStudentAsync_NonExistingStudent_ThrowsKeyNotFoundException()
        {
            // Arrange
            var enrollStudentDTO = new EnrollStudentDTO { CourseID = Guid.NewGuid(), StudentID = Guid.NewGuid() };
            _mockCourseStudentsRepository.Setup(r => r.CourseExistsAsync(enrollStudentDTO.CourseID)).ReturnsAsync(true);
            _mockCourseStudentsRepository.Setup(r => r.StudentExistsAsync(enrollStudentDTO.StudentID)).ReturnsAsync(false);

            // Act and Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _courseStudentsService.EnrollCourseStudentAsync(enrollStudentDTO));
        }

        [Fact]
        public async Task EnrollCourseStudentAsync_AlreadyEnrolled_ThrowsInvalidOperationException()
        {
            // Arrange
            var enrollStudentDTO = new EnrollStudentDTO { CourseID = Guid.NewGuid(), StudentID = Guid.NewGuid() };
            _mockCourseStudentsRepository.Setup(r => r.CourseExistsAsync(enrollStudentDTO.CourseID)).ReturnsAsync(true);
            _mockCourseStudentsRepository.Setup(r => r.StudentExistsAsync(enrollStudentDTO.StudentID)).ReturnsAsync(true);
            _mockCourseStudentsRepository.Setup(r => r.GetCourseStudentByStudentAndCourseAsync(enrollStudentDTO.StudentID, enrollStudentDTO.CourseID)).ReturnsAsync(new CourseStudent());

            // Act and Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _courseStudentsService.EnrollCourseStudentAsync(enrollStudentDTO));
        }
    }
}
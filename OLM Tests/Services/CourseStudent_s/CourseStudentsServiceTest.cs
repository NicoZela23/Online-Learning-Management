using AutoMapper;
using Moq;
using Online_Learning_Management.Domain.Entities.CourseStudent;
using Online_Learning_Management.Domain.Interfaces.CourseStudents;
using Online_Learning_Management.Infrastructure.DTOs.CourseStudents;

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
        public async Task GetCourseStudentByIdAsync_NonExistingId_ThrowsKeyNotFoundException()
        {
            var nonExistingId = Guid.NewGuid();
            _mockCourseStudentsRepository.Setup(r => r.GetCourseStudentByIdAsync(nonExistingId)).ReturnsAsync((CourseStudent)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _courseStudentsService.GetCourseStudentByIdAsync(nonExistingId));
        }

        [Fact]
        public async Task DeleteCourseStudentAsync_ExistingId_DeletesCourseStudent()
        {
            var courseStudentId = Guid.NewGuid();
            var courseStudent = new CourseStudent { Id = courseStudentId };
            _mockCourseStudentsRepository.Setup(r => r.GetCourseStudentByIdAsync(courseStudentId)).ReturnsAsync(courseStudent);
            _mockCourseStudentsRepository.Setup(r => r.DeleteCourseStudentAsync(courseStudentId)).Returns(Task.CompletedTask);

            await _courseStudentsService.DeleteCourseStudentAsync(courseStudentId);

            _mockCourseStudentsRepository.Verify(r => r.DeleteCourseStudentAsync(courseStudentId), Times.Once);
        }

        [Fact]
        public async Task DeleteCourseStudentAsync_NonExistingId_ThrowsKeyNotFoundException()
        {
            var nonExistingId = Guid.NewGuid();
            _mockCourseStudentsRepository.Setup(r => r.GetCourseStudentByIdAsync(nonExistingId)).ReturnsAsync((CourseStudent)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _courseStudentsService.DeleteCourseStudentAsync(nonExistingId));
        }

        [Fact]
        public async Task WithdrawCourseStudentAsync_ExistingStudentAndCourse_DeletesCourseStudent()
        {
            var studentId = Guid.NewGuid();
            var courseId = Guid.NewGuid();
            var courseStudent = new CourseStudent { Id = Guid.NewGuid() };
            _mockCourseStudentsRepository.Setup(r => r.GetCourseStudentByStudentAndCourseAsync(studentId, courseId)).ReturnsAsync(courseStudent);
            _mockCourseStudentsRepository.Setup(r => r.DeleteCourseStudentAsync(courseStudent.Id)).Returns(Task.CompletedTask);

            await _courseStudentsService.WithdrawCourseStudentAsync(studentId, courseId);

            _mockCourseStudentsRepository.Verify(r => r.DeleteCourseStudentAsync(courseStudent.Id), Times.Once);
        }

        [Fact]
        public async Task WithdrawCourseStudentAsync_NonExistingStudentOrCourse_ThrowsKeyNotFoundException()
        {
            var nonExistingStudentId = Guid.NewGuid();
            var courseId = Guid.NewGuid();
            _mockCourseStudentsRepository.Setup(r => r.GetCourseStudentByStudentAndCourseAsync(nonExistingStudentId, courseId)).ReturnsAsync((CourseStudent)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _courseStudentsService.WithdrawCourseStudentAsync(nonExistingStudentId, courseId));
        }

        [Fact]
        public async Task EnrollCourseStudentAsync_ValidEnrollment_EnrollsStudent()
        {
            var enrollStudentDTO = new EnrollStudentDTO { CourseID = Guid.NewGuid(), StudentID = Guid.NewGuid() };
            _mockCourseStudentsRepository.Setup(r => r.CourseExistsAsync(enrollStudentDTO.CourseID)).ReturnsAsync(true);
            _mockCourseStudentsRepository.Setup(r => r.StudentExistsAsync(enrollStudentDTO.StudentID)).ReturnsAsync(true);
            _mockCourseStudentsRepository.Setup(r => r.GetCourseStudentByStudentAndCourseAsync(enrollStudentDTO.StudentID, enrollStudentDTO.CourseID)).ReturnsAsync((CourseStudent)null);
            _mockMapper.Setup(m => m.Map<CourseStudent>(enrollStudentDTO)).Returns(new CourseStudent());

            await _courseStudentsService.EnrollCourseStudentAsync(enrollStudentDTO);

            _mockCourseStudentsRepository.Verify(r => r.AddCourseStudentAsync(It.IsAny<CourseStudent>()), Times.Once);
        }

        [Fact]
        public async Task EnrollCourseStudentAsync_NonExistingCourse_ThrowsKeyNotFoundException()
        {
            var enrollStudentDTO = new EnrollStudentDTO { CourseID = Guid.NewGuid(), StudentID = Guid.NewGuid() };
            _mockCourseStudentsRepository.Setup(r => r.CourseExistsAsync(enrollStudentDTO.CourseID)).ReturnsAsync(false);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _courseStudentsService.EnrollCourseStudentAsync(enrollStudentDTO));
        }

        [Fact]
        public async Task EnrollCourseStudentAsync_NonExistingStudent_ThrowsKeyNotFoundException()
        {
            var enrollStudentDTO = new EnrollStudentDTO { CourseID = Guid.NewGuid(), StudentID = Guid.NewGuid() };
            _mockCourseStudentsRepository.Setup(r => r.CourseExistsAsync(enrollStudentDTO.CourseID)).ReturnsAsync(true);
            _mockCourseStudentsRepository.Setup(r => r.StudentExistsAsync(enrollStudentDTO.StudentID)).ReturnsAsync(false);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _courseStudentsService.EnrollCourseStudentAsync(enrollStudentDTO));
        }

        [Fact]
        public async Task EnrollCourseStudentAsync_AlreadyEnrolled_ThrowsInvalidOperationException()
        {
            var enrollStudentDTO = new EnrollStudentDTO { CourseID = Guid.NewGuid(), StudentID = Guid.NewGuid() };
            _mockCourseStudentsRepository.Setup(r => r.CourseExistsAsync(enrollStudentDTO.CourseID)).ReturnsAsync(true);
            _mockCourseStudentsRepository.Setup(r => r.StudentExistsAsync(enrollStudentDTO.StudentID)).ReturnsAsync(true);
            _mockCourseStudentsRepository.Setup(r => r.GetCourseStudentByStudentAndCourseAsync(enrollStudentDTO.StudentID, enrollStudentDTO.CourseID)).ReturnsAsync(new CourseStudent());

            await Assert.ThrowsAsync<InvalidOperationException>(() => _courseStudentsService.EnrollCourseStudentAsync(enrollStudentDTO));
        }
    }
}
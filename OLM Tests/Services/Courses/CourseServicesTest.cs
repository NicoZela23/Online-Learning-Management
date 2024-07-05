using AutoMapper;
using Moq;
using Online_Learning_Management.Domain.Entities.Courses;

namespace OLM_Tests.Services.Courses
{
    public class CourseServiceTests
    {
        private readonly Mock<ICourseRepository> _mockCourseRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CourseService _courseService;

        public CourseServiceTests()
        {
            _mockCourseRepository = new Mock<ICourseRepository>();
            _mockMapper = new Mock<IMapper>();
            _courseService = new CourseService(_mockCourseRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetCourseByIdAsync_ExistingId_ReturnsCourse()
        {
            var courseId = Guid.NewGuid();
            var courseEntity = new Course
            {
                Id = Guid.NewGuid(),
                Title = "Introduction to Programming",
                Description = "Learn the basics of programming languages.",
                IdInstructor = Guid.NewGuid(),
                Content = new List<string> { "Variables", "Loops", "Functions" },
                DurationInWeeks = 6
            };
            _mockCourseRepository.Setup(r => r.GetCourseByIdAsync(courseId)).ReturnsAsync(courseEntity);
            _mockMapper.Setup(m => m.Map<Course>(courseEntity)).Returns(new Course
            {
                Id = Guid.NewGuid(),
                Title = "Introduction to Programming",
                Description = "Learn the basics of programming languages.",
                IdInstructor = Guid.NewGuid(),
                Content = new List<string> { "Variables", "Loops", "Functions" },
                DurationInWeeks = 6
            });

            var result = await _courseService.GetCourseByIdAsync(courseId);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetCourseByIdAsync_NonExistingId_ThrowsKeyNotFoundException()
        {
            var nonExistingId = Guid.NewGuid();
            _mockCourseRepository.Setup(r => r.GetCourseByIdAsync(nonExistingId)).ReturnsAsync((Course)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _courseService.GetCourseByIdAsync(nonExistingId));
        }

        [Fact]
        public async Task UpdateCourseAsync_NonExistingCourse_ThrowsKeyNotFoundException()
        {
            var nonExistingCourseId = Guid.NewGuid();
            var updateCourseDto = new UpdateCourseDTO();
            _mockCourseRepository.Setup(r => r.GetCourseByIdAsync(nonExistingCourseId)).ReturnsAsync((Course)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _courseService.UpdateCourseAsync(nonExistingCourseId, updateCourseDto));
        }

        [Fact]
        public async Task DeleteCourseAsync_ExistingCourse_DeletesCourse()
        {
            var courseId = Guid.NewGuid();
            var courseEntity = new Course
            {
                Id = Guid.NewGuid(),
                Title = "Introduction to Programming",
                Description = "Learn the basics of programming languages.",
                IdInstructor = Guid.NewGuid(),
                Content = new List<string> { "Variables", "Loops", "Functions" },
                DurationInWeeks = 6
            };
            _mockCourseRepository.Setup(r => r.GetCourseByIdAsync(courseId)).ReturnsAsync(courseEntity);
            _mockCourseRepository.Setup(r => r.DeleteCourseAsync(courseId)).Returns(Task.CompletedTask);

            await _courseService.DeleteCourseAsync(courseId);

            _mockCourseRepository.Verify(r => r.DeleteCourseAsync(courseId), Times.Once);
        }

        [Fact]
        public async Task DeleteCourseAsync_NonExistingCourse_ThrowsKeyNotFoundException()
        {
            var nonExistingCourseId = Guid.NewGuid();
            _mockCourseRepository.Setup(r => r.GetCourseByIdAsync(nonExistingCourseId)).ReturnsAsync((Course)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _courseService.DeleteCourseAsync(nonExistingCourseId));
        }
    }
}

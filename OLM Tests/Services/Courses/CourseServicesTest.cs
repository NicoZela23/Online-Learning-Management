using AutoMapper;
using Moq;
using Online_Learning_Management.Domain.Entities.Courses;
using Online_Learning_Management.Domain.Entities.Instructors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task CreateCourseAsync_ValidInput_ReturnsCreatedCourse()
        {
            // Arrange
            var createCourseDto = new CreateCourseDTO();
            var mappedCourse = new Course
            {
                Id = Guid.NewGuid(),
                Title = "Introduction to Programming",
                Description = "Learn the basics of programming languages.",
                IdInstructor = 123,
                Content = new List<string> { "Variables", "Loops", "Functions" },
                DurationInWeeks = 6
            };

            _mockMapper.Setup(m => m.Map<Course>(createCourseDto)).Returns(mappedCourse);
            _mockCourseRepository.Setup(r => r.CreateCourseAsync(mappedCourse)).ReturnsAsync(mappedCourse);

            var result = await _courseService.CreateCourseAsync(createCourseDto);

            Assert.Equal(mappedCourse, result);
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
                IdInstructor = 123,
                Content = new List<string> { "Variables", "Loops", "Functions" },
                DurationInWeeks = 6
            };
            _mockCourseRepository.Setup(r => r.GetCourseByIdAsync(courseId)).ReturnsAsync(courseEntity);
            _mockMapper.Setup(m => m.Map<Course>(courseEntity)).Returns(new Course
            {
                Id = Guid.NewGuid(),
                Title = "Introduction to Programming",
                Description = "Learn the basics of programming languages.",
                IdInstructor = 123,
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
        public async Task GetCoursesByIdInstructorAsync_ExistingId_ReturnsListOfCourses()
        {
            var instructorId = 1;
            var courseEntities = new List<Course> { new Course
            {
                Id = Guid.NewGuid(),
                Title = "Introduction to Programming",
                Description = "Learn the basics of programming languages.",
                IdInstructor = 123,
                Content = new List<string> { "Variables", "Loops", "Functions" },
                DurationInWeeks = 6
            }, new Course
            {
                Id = Guid.NewGuid(),
                Title = "Introduction to Programming",
                Description = "Learn the basics of programming languages.",
                IdInstructor = 123,
                Content = new List<string> { "Variables", "Loops", "Functions" },
                DurationInWeeks = 6
            }
            };
            _mockCourseRepository.Setup(r => r.GetCoursesByIdInstructorAsync(instructorId)).ReturnsAsync(courseEntities);
            _mockMapper.Setup(m => m.Map<IEnumerable<Course>>(courseEntities)).Returns(courseEntities);

            var result = await _courseService.GetCoursesByIdInstructorAsync(instructorId);

            Assert.Equal(courseEntities.Count, result.Count());
        }

        [Fact]
        public async Task UpdateCourseAsync_ExistingCourseAndInstructor_ReturnsUpdatedCourse()
        {
            var courseId = Guid.NewGuid();
            var updateCourseDto = new UpdateCourseDTO { IdInstructor = 1 }; // Provide valid DTO
            var courseEntity = new Course
            {
                Id = Guid.NewGuid(),
                Title = "Introduction to Programming",
                Description = "Learn the basics of programming languages.",
                IdInstructor = 123,
                Content = new List<string> { "Variables", "Loops", "Functions" },
                DurationInWeeks = 6
            };
            _mockCourseRepository.Setup(r => r.GetCourseByIdAsync(courseId)).ReturnsAsync(courseEntity);
            _mockCourseRepository.Setup(r => r.InstructorExistsAsync(updateCourseDto.IdInstructor)).ReturnsAsync(true);
            _mockMapper.Setup(m => m.Map(updateCourseDto, courseEntity)).Verifiable();
            _mockMapper.Setup(m => m.Map<Course>(courseEntity)).Returns(new Course
            {
                Id = Guid.NewGuid(),
                Title = "Introduction to Programming",
                Description = "Learn the basics of programming languages.",
                IdInstructor = 123,
                Content = new List<string> { "Variables", "Loops", "Functions" },
                DurationInWeeks = 6
            });

            var result = await _courseService.UpdateCourseAsync(courseId, updateCourseDto);

            Assert.NotNull(result);
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
        public async Task UpdateCourseAsync_NonExistingInstructor_ThrowsKeyNotFoundException()
        {
            var courseId = Guid.NewGuid();
            var updateCourseDto = new UpdateCourseDTO { IdInstructor = 1 }; // Provide invalid instructor ID
            var courseEntity = new Course
            {
                Id = Guid.NewGuid(),
                Title = "Introduction to Programming",
                Description = "Learn the basics of programming languages.",
                IdInstructor = 123,
                Content = new List<string> { "Variables", "Loops", "Functions" },
                DurationInWeeks = 6
            };
            _mockCourseRepository.Setup(r => r.GetCourseByIdAsync(courseId)).ReturnsAsync(courseEntity);
            _mockCourseRepository.Setup(r => r.InstructorExistsAsync(updateCourseDto.IdInstructor)).ReturnsAsync(false);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _courseService.UpdateCourseAsync(courseId, updateCourseDto));
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
                IdInstructor = 123,
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

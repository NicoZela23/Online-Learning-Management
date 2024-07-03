using AutoMapper;
using Online_Learning_Management.Domain.Entities.Courses;
using OnlineLearningManagement.Application.Courses.Validator;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;



    public CourseService(ICourseRepository courseRepository, IMapper mapper)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Course>> GetAllCoursesAsync()
    {
        var courses = await _courseRepository.GetAllCoursesAsync();
        return _mapper.Map<IEnumerable<Course>>(courses);
    }

    public async Task<Course> CreateCourseAsync(CreateCourseDTO courseDto)
    {
        var validator = new CreateCourseValidator();
        var validationResult = await validator.ValidateAsync(courseDto);

        if (!validationResult.IsValid)
        {
            var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ArgumentException(errorMessages);
        }

        if (!await _courseRepository.InstructorExistsAsync(courseDto.IdInstructor))
        {
            throw new KeyNotFoundException($"Instructor with Id {courseDto.IdInstructor} not found or does not exist");
        }

        var course = _mapper.Map<Course>(courseDto);
        return await _courseRepository.CreateCourseAsync(course);
    }

    public async Task<Course> GetCourseByIdAsync(Guid Id)
    {
        var course = await _courseRepository.GetCourseByIdAsync(Id);
        if (course == null)
        {
            throw new KeyNotFoundException("Course not found");
        }
        return _mapper.Map<Course>(course);
    }

    public async Task<Course> UpdateCourseAsync(Guid courseId, UpdateCourseDTO courseDto)
    {
        var course = await _courseRepository.GetCourseByIdAsync(courseId);

        if (course == null)
        {
            throw new KeyNotFoundException("Course not found");
        }

        var validator = new UpdateCourseValidator();
        var validationResult = await validator.ValidateAsync(courseDto);

        if (!validationResult.IsValid)
        {
            var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ArgumentException(errorMessages);
        }

        bool instructorExists = await _courseRepository.InstructorExistsAsync(courseDto.IdInstructor);
        if (!instructorExists)
        {
            throw new KeyNotFoundException($"Instructor with Id {courseDto.IdInstructor} not found");
        }

        _mapper.Map(courseDto, course);
        await _courseRepository.UpdateCourseAsync(course);
        return _mapper.Map<Course>(course);
    }

    public async Task DeleteCourseAsync(Guid courseId)
    {
        var course = await _courseRepository.GetCourseByIdAsync(courseId);
        if (course == null)
        {
            throw new KeyNotFoundException("Course not found");
        }
        await _courseRepository.DeleteCourseAsync(courseId);
    }


}
using AutoMapper;
using Online_Learning_Management.Domain.Entities;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;



    public CourseService(ICourseRepository courseRepository, IMapper mapper)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public async Task<Course> CreateCourseAsync(CreateCourseDTO courseDto)
    {
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


    // Add more methods here
}
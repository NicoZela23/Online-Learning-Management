
using AutoMapper;
using Online_Learning_Management.Application.CourseStudents;
using Online_Learning_Management.Domain.Entities.CourseStudent;
using Online_Learning_Management.Domain.Interfaces.CourseStudents;
using Online_Learning_Management.Infrastructure.DTOs.CourseStudents;




public class CourseStudentsService : ICourseStudentsService
{
    private readonly ICourseStudentsRepository _courseStudentsRepository;
    private readonly IMapper _mapper;

    public CourseStudentsService(ICourseStudentsRepository courseStudentsRepository, IMapper mapper)
    {
        _courseStudentsRepository = courseStudentsRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CourseStudentDTO>> GetAllCourseStudentsAsync()
    {
        var courseStudents = await _courseStudentsRepository.GetAllCourseStudentsAsync();
        return _mapper.Map<IEnumerable<CourseStudentDTO>>(courseStudents);
    }

    public async Task<CourseStudentDTO> GetCourseStudentByIdAsync(Guid id)
    {
        var courseStudent = await _courseStudentsRepository.GetCourseStudentByIdAsync(id);
        if (courseStudent == null)
        {
            throw new KeyNotFoundException("CourseStudent not found");
        }
        return _mapper.Map<CourseStudentDTO>(courseStudent);
    }

    public async Task DeleteCourseStudentAsync(Guid id)
    {
        var courseStudent = await _courseStudentsRepository.GetCourseStudentByIdAsync(id);
        if (courseStudent == null)
        {
            throw new KeyNotFoundException("CourseStudent not found");
        }
        await _courseStudentsRepository.DeleteCourseStudentAsync(id);
    }

    // new method to add a student to a course
    public async Task WithdrawCourseStudentAsync(Guid studentId, Guid courseId)
    {
        var courseStudent = await _courseStudentsRepository.GetCourseStudentByStudentAndCourseAsync(studentId, courseId);
        if (courseStudent == null)
        {
            throw new KeyNotFoundException("The student is not enrolled in the course");
        }
        await _courseStudentsRepository.DeleteCourseStudentAsync(courseStudent.Id);
    }

    public async Task EnrollCourseStudentAsync(EnrollStudentDTO enrollStudentDTO)
    {
        var courseStudent = _mapper.Map<CourseStudent>(enrollStudentDTO);
        await _courseStudentsRepository.AddCourseStudentAsync(courseStudent);
    }


}

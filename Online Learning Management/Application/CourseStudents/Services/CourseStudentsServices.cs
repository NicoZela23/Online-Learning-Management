
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
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

    public async Task<IEnumerable<CourseStudent>> GetAllCourseStudentsAsync()
    {
        var courseStudents = await _courseStudentsRepository.GetAllCourseStudentsAsync();
        return _mapper.Map<IEnumerable<CourseStudent>>(courseStudents);
    }

    public async Task<CourseStudent> GetCourseStudentByIdAsync(Guid id)
    {
        var courseStudent = await _courseStudentsRepository.GetCourseStudentByIdAsync(id);
        if (courseStudent == null)
        {
            throw new KeyNotFoundException("CourseStudent not found");
        }
        return _mapper.Map<CourseStudent>(courseStudent);
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
        var courseExists = await _courseStudentsRepository.CourseExistsAsync(enrollStudentDTO.CourseID);
        var studentExists = await _courseStudentsRepository.StudentExistsAsync(enrollStudentDTO.StudentID);
        if (!courseExists)
        {
            throw new KeyNotFoundException("Course not found or does not exist");
        }
        if (!studentExists)
        {
            throw new KeyNotFoundException("Student not found or does not exist");
        }
        var alreadyEnrolled = await _courseStudentsRepository.GetCourseStudentByStudentAndCourseAsync(enrollStudentDTO.StudentID, enrollStudentDTO.CourseID);
        if (alreadyEnrolled != null)
        {
            throw new InvalidOperationException("The student is already enrolled in the course");
        }
        var courseStudent = _mapper.Map<CourseStudent>(enrollStudentDTO);
        await _courseStudentsRepository.AddCourseStudentAsync(courseStudent);
    }


}

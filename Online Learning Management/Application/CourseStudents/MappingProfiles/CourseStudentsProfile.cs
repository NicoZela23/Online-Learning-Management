using AutoMapper;
using Online_Learning_Management.Domain.Entities.CourseStudent;
using Online_Learning_Management.Infrastructure.DTOs.CourseStudents;

namespace Online_Learning_Management.Application.CourseStudents.MappingProfiles
{
    public class CourseStudentsProfile : Profile
    {
        public CourseStudentsProfile()
        {
            CreateMap<CourseStudent, CourseStudentDTO>();
        }
    }
}

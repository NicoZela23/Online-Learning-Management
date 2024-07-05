using AutoMapper;
using Online_Learning_Management.Domain.Entities.ReportCourses;
using Online_Learning_Management.Infrastructure.DTOs.ReportCourse;

namespace Online_Learning_Management.Application.ReportCourses.MappingProfiles
{
    public class ReportCourseProfile : Profile
    {
        public ReportCourseProfile() 
        {
            CreateMap<ReportCourse, ReportCourseDTO>();
        }
    }
}

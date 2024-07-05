using AutoMapper;
using TaskStudentEntity = Online_Learning_Management.Domain.Entities.TaskStudents.TaskStudent;
using Online_Learning_Management.Infrastructure.DTOs.TaskStudent;

namespace Online_Learning_Management.Application.TaskStudent.MappingProfiles
{
    public class TaskStudentProfile : Profile
    {
        public TaskStudentProfile()
        {
            CreateMap<CreateTaskStudentDTO, TaskStudentEntity>()
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.ModuleTaskID, opt => opt.MapFrom(src => src.ModuleTaskID))
               .ForMember(dest => dest.StudentID, opt => opt.MapFrom(src => src.StudentID))
               .ForMember(dest => dest.FileID, opt => opt.MapFrom(src => src.FileID))
               .ForMember(dest => dest.UploadDate, opt => opt.Ignore());
        }
    }
}

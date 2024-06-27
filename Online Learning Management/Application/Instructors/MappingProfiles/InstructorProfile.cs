using AutoMapper;
using Online_Learning_Management.Domain.Entities.Instructors;
using Online_Learning_Management.Infrastructure.DTOs.Instructor;

namespace Online_Learning_Management.Application.Instructors.MappingProfiles
{
    public class InstructorProfile : Profile
    {
        public InstructorProfile() 
        {
            CreateMap<CreateInstructorDTO, Instructor>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateInstructorDTO, Instructor>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != null))
                .ForMember(dest => dest.LastName, opt => opt.Condition(src => src.LastName != null))
                .ForMember(dest => dest.Email, opt => opt.Condition(src => src.Email != null))
                .ForMember(dest => dest.CreateAt, opt => opt.Ignore());
        }
    }
}

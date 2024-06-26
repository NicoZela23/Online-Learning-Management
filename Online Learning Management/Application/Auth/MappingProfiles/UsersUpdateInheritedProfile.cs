using AutoMapper;
using Online_Learning_Management.Infrastructure.DTOs.Auth;
using Online_Learning_Management.Infrastructure.DTOs.Instructor;
using Online_Learning_Management.Infrastructure.DTOs.Student;

namespace Online_Learning_Management.Application.Auth.MappingProfiles
{
    public class UsersUpdateInheritedProfile : Profile
    {
        public UsersUpdateInheritedProfile() 
        {
            CreateMap<UpdateUserDTO, UpdateStudentDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EmailAddress));

            CreateMap<UpdateUserDTO, UpdateInstructorDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EmailAddress));
        }
    }
}

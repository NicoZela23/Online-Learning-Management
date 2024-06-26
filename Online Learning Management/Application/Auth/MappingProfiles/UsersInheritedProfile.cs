using AutoMapper;
using Online_Learning_Management.Domain.Entities.Auth;
using Online_Learning_Management.Infrastructure.DTOs.Instructor;
using Online_Learning_Management.Infrastructure.DTOs.Student;

namespace Online_Learning_Management.Application.Auth.MappingProfiles
{
    public class UsersInheritedProfile : Profile
    {
        public UsersInheritedProfile()
        {
            CreateMap<User, CreateStudentDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EmailAddress));

            CreateMap<User, CreateInstructorDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EmailAddress));

        }
    }
}

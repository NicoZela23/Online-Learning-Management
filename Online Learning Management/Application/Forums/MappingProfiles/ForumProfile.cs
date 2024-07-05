using AutoMapper;
using Online_Learning_Management.Domain.Entities.Forums;
using Online_Learning_Management.Infrastructure.DTOs.Forum;
using Online_Learning_Management.Infrastructure.DTOs.Module;

namespace Online_Learning_Management.Application.Forums.MappingProfiles
{
    public class ForumProfile:Profile
    {
        public ForumProfile()
        {
            CreateMap<CreateForumDTO, Forum>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.CourseID, opt => opt.MapFrom(src => src.CourseID))
               .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<UpdateForumDTO, Forum>()
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.CourseID, opt => opt.Ignore())
               .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}

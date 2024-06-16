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
               .ForMember(dest => dest.Id, opt => opt.Ignore());
            
            CreateMap<UpdateForumDTO, Forum>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CourseID, opt => opt.Ignore());
        }
    }
}

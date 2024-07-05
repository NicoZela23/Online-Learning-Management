using AutoMapper;
using Online_Learning_Management.Domain.Entities.Post;
using Online_Learning_Management.Infrastructure.DTOs.Post;

namespace Online_Learning_Management.Application.Post.PostProfile
{
   public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<CreatePostDTO, Posts>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Forum, opt => opt.Ignore());

        CreateMap<UpdatePostDTO, Posts>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ForumId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Forum, opt => opt.Ignore());
    }
}

}

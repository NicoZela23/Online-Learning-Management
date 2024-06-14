using AutoMapper;
using Online_Learning_Management.Domain.Entities.Modules;
using Online_Learning_Management.Infrastructure.DTOs.Module;
namespace Online_Learning_Management.Application.Modules.MappingProfiles
{
    public class ModuleProfile : Profile
    {
        public ModuleProfile() 
        {
            CreateMap<CreateModuleDTO, Module>()
               .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdateModuleDTO, Module>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CourseID, opt => opt.Ignore());
        }
    }
}

using AutoMapper;
using Online_Learning_Management.Domain.Entities.ModuleTasks;
using Online_Learning_Management.Infrastructure.DTOs.ModuleTask;

namespace Online_Learning_Management.Application.ModuleTasks.MappingProfiles
{
    public class ModuleTaskProfile : Profile
    {
        public ModuleTaskProfile() 
        {
            CreateMap<CreateModuleTaskDTO, ModuleTask>()
               .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdateModuleTaskDTO, ModuleTask>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ModuleID, opt => opt.Ignore());
        }
    }
}

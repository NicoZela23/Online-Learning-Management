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
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.ModuleID, opt => opt.MapFrom(src => src.ModuleID))
               .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
               .ForMember(dest => dest.Deadline, opt => opt.MapFrom(src => src.Deadline));

            CreateMap<UpdateModuleTaskDTO, ModuleTask>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ModuleID, opt => opt.Ignore())
                .ForMember(dest => dest.Title, opt => opt.Condition(src => src.Title != null))
                .ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != null))
                .ForMember(dest => dest.Type, opt => opt.Condition(src => src.Type != null))
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
                .ForMember(dest => dest.Deadline, opt => opt.Condition(src => src.Deadline != null));
        }
    }
}

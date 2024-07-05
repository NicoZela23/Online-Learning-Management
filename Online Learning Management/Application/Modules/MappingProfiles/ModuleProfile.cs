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
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CourseID, opt => opt.MapFrom(src => src.CourseID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.LearningOutcomes, opt => opt.MapFrom(src => src.LearningOutcomes))
                .ForMember(dest => dest.Prerequisites, opt => opt.MapFrom(src => src.Prerequisites))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Resources, opt => opt.MapFrom(src => src.Resources))
                .ForMember(dest => dest.AssessmentMethods, opt => opt.MapFrom(src => src.AssessmentMethods));

            CreateMap<UpdateModuleDTO, Module>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CourseID, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != null))
                .ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != null))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.LearningOutcomes, opt => opt.MapFrom(src => src.LearningOutcomes))
                .ForMember(dest => dest.Prerequisites, opt => opt.MapFrom(src => src.Prerequisites))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Resources, opt => opt.MapFrom(src => src.Resources))
                .ForMember(dest => dest.AssessmentMethods, opt => opt.MapFrom(src => src.AssessmentMethods));
        }
    }
}

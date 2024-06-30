using AutoMapper;
using Online_Learning_Management.Domain.Entities.ModuleProgresses;
using Online_Learning_Management.Infrastructure.DTOs.ModuleProgresses;

namespace Online_Learning_Management.Application.Mappings
{
    public class ModuleProgressProfile : Profile
    {
        public ModuleProgressProfile()
        {
            CreateMap<CreateModuleProgressDTO, ModuleProgress>();

            CreateMap<UpdateModuleProgressDTO, ModuleProgress>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); 
        }
    }
}
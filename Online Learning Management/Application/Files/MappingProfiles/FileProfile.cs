using AutoMapper;
using Online_Learning_Management.Domain.Entities.Files;
using Online_Learning_Management.Infrastructure.DTOs.File;

namespace Online_Learning_Management.Application.Files.MappingProfiles
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<CreateFileDTO, FileMetadata>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.FileName))
                .ForMember(dest => dest.BlobURL, opt => opt.MapFrom(src => src.BlobURL))
                .ForMember(dest => dest.FileSize, opt => opt.MapFrom(src => src.FileSize))
                .ForMember(dest => dest.UploadedAt, opt => opt.MapFrom(src => src.UploadedAt));

            CreateMap<(IFormFile file, string blobUrl), CreateFileDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.file.FileName))
                .ForMember(dest => dest.BlobURL, opt => opt.MapFrom(src => src.blobUrl))
                .ForMember(dest => dest.FileSize, opt => opt.MapFrom(src => src.file.Length / 1024f / 1024f))
                .ForMember(dest => dest.UploadedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}

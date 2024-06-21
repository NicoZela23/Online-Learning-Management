using Azure.Storage.Blobs.Models;
using Online_Learning_Management.Domain.Entities.Files;
using Online_Learning_Management.Infrastructure.DTOs.File;

namespace Online_Learning_Management.Domain.Interfaces.Files
{
    public interface IFileService
    {
        Task<BlobContentInfo> UploadFileAsync(IFormFile file);
        Task<FileMetadata> GetFileDataByIdAsync(Guid id);
        Task<IEnumerable<FileMetadata>> GetAllFileDataAsync();
        Task AddFileDataAsync(CreateFileDTO data);
        Task DeleteFileDataAsync(Guid id);
        Task<FileMetadata> UploadAndAddFileAsync(IFormFile file);
    }
}

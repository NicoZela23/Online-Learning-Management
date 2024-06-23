using Online_Learning_Management.Domain.Entities.Files;

namespace Online_Learning_Management.Domain.Interfaces.Files
{
    public interface IFileRepository
    {
        Task<FileMetadata> GetFileDataByIdAsync(Guid id);
        Task<IEnumerable<FileMetadata>> GetAllFileDataAsync();
        Task<FileMetadata> AddFileDataAsync(FileMetadata data);
        Task DeleteFileDataAsync(Guid id);
    }
}

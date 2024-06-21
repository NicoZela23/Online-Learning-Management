using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities.Files;
using Online_Learning_Management.Domain.Interfaces.Files;
using Online_Learning_Management.Infrastructure.Data;

namespace Online_Learning_Management.Infrastructure.Repositories.Files
{
    public class FileRepository : IFileRepository
    {
        private readonly ApplicationDbContext _context;
        public FileRepository(ApplicationDbContext context) 
        { 
            _context = context;
        }
        public async Task AddFileDataAsync(FileMetadata data)
        {
            await _context.UploadedFiles.AddAsync(data);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteFileDataAsync(Guid id)
        {
            var file = _context.UploadedFiles.Find(id);
            if(file != null)
            {
                _context.UploadedFiles.Remove(file);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<FileMetadata>> GetAllFileDataAsync()
        {
            return await _context.UploadedFiles.ToListAsync();
        }

        public async Task<FileMetadata> GetFileDataByIdAsync(Guid id)
        {
            return await _context.UploadedFiles.FindAsync(id);
        }
    }
}

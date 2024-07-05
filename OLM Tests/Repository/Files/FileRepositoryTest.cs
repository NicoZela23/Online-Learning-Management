using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities.Files;
using Online_Learning_Management.Infrastructure.Data;
using Online_Learning_Management.Infrastructure.Repositories.Files;

namespace OLM_Tests.Repository.Files
{
    public class FileRepositoryTest
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        private FileMetadata CreateTestFileMetadata()
        {
            return new FileMetadata
            {
                Id = Guid.NewGuid(),
                FileName = "testfile.txt",
                BlobURL = "lerasdnainiasd.url.com/testfile.txt",
                FileSize = 1024,
                UploadedAt = DateTime.UtcNow
            };
        }

        [Fact]
        public async Task AddFileDataAsync_ShouldAddFileData()
        {
            var context = GetDbContext();
            var repository = new FileRepository(context);
            var fileMetadata = CreateTestFileMetadata();

            await repository.AddFileDataAsync(fileMetadata);

            var result = await context.UploadedFiles.FindAsync(fileMetadata.Id);
            Assert.NotNull(result);
            Assert.Equal(fileMetadata.FileName, result.FileName);
        }

        [Fact]
        public async Task GetFileDataByIdAsync_ShouldReturnFileData()
        {
            var context = GetDbContext();
            var fileMetadata = CreateTestFileMetadata();
            await context.UploadedFiles.AddAsync(fileMetadata);
            await context.SaveChangesAsync();

            var repository = new FileRepository(context);
            var result = await repository.GetFileDataByIdAsync(fileMetadata.Id);

            Assert.NotNull(result);
            Assert.Equal(fileMetadata.FileName, result.FileName);
        }

        [Fact]
        public async Task GetAllFileDataAsync_ShouldReturnAllFileData()
        {
            var context = GetDbContext();
            var fileMetadata1 = CreateTestFileMetadata();
            var fileMetadata2 = CreateTestFileMetadata();
            await context.UploadedFiles.AddRangeAsync(fileMetadata1, fileMetadata2);
            await context.SaveChangesAsync();

            var repository = new FileRepository(context);
            var result = await repository.GetAllFileDataAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task DeleteFileDataAsync_ShouldRemoveFileData()
        {
            var context = GetDbContext();
            var fileMetadata = CreateTestFileMetadata();
            await context.UploadedFiles.AddAsync(fileMetadata);
            await context.SaveChangesAsync();

            var repository = new FileRepository(context);
            await repository.DeleteFileDataAsync(fileMetadata.Id);

            var result = await context.UploadedFiles.FindAsync(fileMetadata.Id);
            Assert.Null(result);
        }
    }
}

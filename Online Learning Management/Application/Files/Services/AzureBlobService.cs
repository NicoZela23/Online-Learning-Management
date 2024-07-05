using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Online_Learning_Management.Application.Files.Validator;
using Online_Learning_Management.Domain.Entities.Files;
using Online_Learning_Management.Domain.Interfaces.Files;
using Online_Learning_Management.Infrastructure.DTOs.File;

namespace Online_Learning_Management.Application.Files.Services
{
    public class AzureBlobService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;   

        BlobServiceClient _serviceClient;
        BlobContainerClient _containerClient;
        string? azureConnectionString;

        public AzureBlobService(IConfiguration configuration, IFileRepository fileRepository, IMapper mapper)
        {
            azureConnectionString = configuration.GetConnectionString("AzureBlobStorage");
            _serviceClient = new BlobServiceClient(azureConnectionString);
            _containerClient = _serviceClient.GetBlobContainerClient(configuration["BlobContainerName"]);
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        public async Task <FileMetadata>AddFileDataAsync(CreateFileDTO data)
        {
            var validator = new CreateFileValidator();
            var validationResult = await validator.ValidateAsync(data);

            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException(errorMessages);
            }

            var file = _mapper.Map<FileMetadata>(data);
            var fileCreated = await _fileRepository.AddFileDataAsync(file);
            return fileCreated;
        }

        public async Task DeleteFileDataAsync(Guid id)
        {
            var file = await _fileRepository.GetFileDataByIdAsync(id);
            if (file == null) 
            {
                throw new ArgumentException();
            }
            await _fileRepository.DeleteFileDataAsync(id);
        }

        public async Task<IEnumerable<FileMetadata>> GetAllFileDataAsync()
        {
            var files = await _fileRepository.GetAllFileDataAsync();
            return _mapper.Map<IEnumerable<FileMetadata>>(files);
        }

        public async Task<FileMetadata> GetFileDataByIdAsync(Guid id)
        {
            var selectedData = await _fileRepository.GetFileDataByIdAsync(id);
            if(selectedData == null)
            {
                throw new ArgumentException();
            }

            return _mapper.Map<FileMetadata>(selectedData);
        }

        public async Task<BlobContentInfo> UploadFileAsync(IFormFile file)
        {
            string fileName = file.FileName;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                var response = await _containerClient.UploadBlobAsync(fileName, memoryStream, default);
                return response.Value;
            }
        }

        public async Task<FileMetadata> UploadAndAddFileAsync(IFormFile file)
        {
            var blobClient = _containerClient.GetBlobClient(file.FileName);
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            var blobUrl = blobClient.Uri.ToString();
            var createFileDTO = _mapper.Map<CreateFileDTO>((file, blobUrl));
            await AddFileDataAsync(createFileDTO);
            var fileMetadata = _mapper.Map<FileMetadata>(createFileDTO);
            return fileMetadata;
        }
    }
}

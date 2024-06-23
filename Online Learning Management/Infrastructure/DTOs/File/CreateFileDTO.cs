namespace Online_Learning_Management.Infrastructure.DTOs.File
{
    public class CreateFileDTO
    {
        public Guid Id { get; set; }
        public string? FileName { get; set; }
        public string? BlobURL { get; set; }
        public float FileSize { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}

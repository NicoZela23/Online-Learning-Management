namespace Online_Learning_Management.Domain.Entities.Files
{
    public class FileMetadata
    {
        public Guid Id { get; set; }
        public string? FileName { get; set; }
        public string? BlobURL{ get; set; }

        public float FileSize { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}

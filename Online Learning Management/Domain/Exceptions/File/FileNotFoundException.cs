namespace Online_Learning_Management.Domain.Exceptions.File
{
    public class FileNotFoundException : Exception
    {
        public FileNotFoundException() : base("File not found") { }
    }
}

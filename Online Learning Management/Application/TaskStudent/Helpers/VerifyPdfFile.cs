namespace Online_Learning_Management.Application.TaskStudent.helpers
{
    public class VerifyPdfFile
    {
        private const int MaxFileSize = 2 * 1024 * 1024; // 2 MB en bytes

        public static bool IsPdf(IFormFile file)
        {
            return file.ContentType == "application/pdf";
        }

        public static bool IsWithinFileSizeLimit(IFormFile file)
        {
            return file.Length <= MaxFileSize;
        }

        public static string ValidateFile(IFormFile file)
        {
            if (file == null)
            {
                return "The file is required.";
            }

            if (!IsPdf(file))
            {
                return "The file must be of type PDF.";
            }

            if (!IsWithinFileSizeLimit(file))
            {
                return "The file size should not exceed 2 MB.";
            }

            return null;
        }
    }
}

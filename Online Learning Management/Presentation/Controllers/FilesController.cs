using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Management.Domain.Entities.Files;
using Online_Learning_Management.Domain.Interfaces.Files;

namespace Online_Learning_Management.Presentation.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileMetadata>>> GetAllFilesData()
        {
            try
            {
                var files = await _fileService.GetAllFileDataAsync();
                return Ok(files);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FileMetadata>> GetFileDataById(Guid id)
        {
            try
            {
                var file = await _fileService.GetFileDataByIdAsync(id);
                return Ok(file);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFileMetadata(Guid id)
        {
            try
            { 
                await _fileService.DeleteFileDataAsync(id);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound("File not found.");
            }
        }
        [HttpPost]
        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            try
            {
                var fileMetadata = await _fileService.UploadAndAddFileAsync(file);
                return Ok(fileMetadata);
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
        }
    } 
}

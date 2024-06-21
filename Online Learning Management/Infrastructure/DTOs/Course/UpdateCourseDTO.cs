using System.ComponentModel.DataAnnotations;

public class UpdateCourseDTO
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "IdInstructor must be greater than 0")]
    public int IdInstructor { get; set; }

    [Required]
    public List<string> Content { get; set; }

    [Required]
    [Range(1, 10, ErrorMessage = "DurationInWeeks must be greater than 0")]
    public int DurationInWeeks { get; set; }
}
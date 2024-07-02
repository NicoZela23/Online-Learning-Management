using System.ComponentModel.DataAnnotations;

public class UpdateCourseDTO
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }

    [Required]
    public Guid IdInstructor { get; set; }

    [Required]
    public List<string> Content { get; set; }

    [Required]
    [Range(1, 10, ErrorMessage = "DurationInWeeks must be greater than 0 or less or equal to 10")]
    public int DurationInWeeks { get; set; }
}
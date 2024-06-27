namespace Online_Learning_Management.Infrastructure.DTOs.Instructor
{
    public class CreateInstructorDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime? CreateAt { get; set; }
    }
}

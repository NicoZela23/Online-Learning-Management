namespace Online_Learning_Management.Infrastructure.DTOs.Auth
{
    public class CreateUserDTO
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? EmailAddress { get; set; }
        public string? Role { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

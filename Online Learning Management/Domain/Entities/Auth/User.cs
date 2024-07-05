namespace Online_Learning_Management.Domain.Entities.Auth
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public string? EmailAddress { get; set; }
        public string? Role { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

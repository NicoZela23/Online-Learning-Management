﻿namespace Online_Learning_Management.Infrastructure.DTOs.Auth
{
    public class UpdateUserDTO
    {
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public string? EmailAddress { get; set; }
    }
}

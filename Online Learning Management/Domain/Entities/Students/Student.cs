﻿namespace Online_Learning_Management.Domain.Entities.Students
{
    public class Student
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateOnly CreateAt { get; set; }
        public DateOnly UpdateAt { get; set; }

    }
}
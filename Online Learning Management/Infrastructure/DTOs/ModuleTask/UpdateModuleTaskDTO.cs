﻿using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Management.Infrastructure.DTOs.ModuleTask
{
    public class UpdateModuleTaskDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public DateTime? Deadline { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Management.Infrastructure.DTOs.Module
{
    public class UpdateModuleDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public TimeSpan Duration { get; set; }
        public List<string>? LearningOutcomes { get; set; }
        public List<string>? Prerequisites { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public List<string>? Resources { get; private set; }
        public Dictionary<string, int>? AssessmentMethods { get; set; }
    }
}

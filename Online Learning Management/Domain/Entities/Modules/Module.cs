namespace Online_Learning_Management.Domain.Entities.Modules
{
    public class Module
    {
        public Guid Id { get; set; }
        public Guid? CourseID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public TimeSpan Duration { get; set; }
        public List<string>? LearningOutcomes { get; set; }
        public List<string>? Prerequisites { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public List<string>? Resources { get; set; }
        public Dictionary<string, int>? AssessmentMethods{get; set;}
    }
}
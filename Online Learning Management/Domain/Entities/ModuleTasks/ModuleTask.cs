namespace Online_Learning_Management.Domain.Entities.ModuleTasks
{
    public class ModuleTask
    {
        public Guid Id { get; set; }
        public Guid ModuleID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateOnly? DateCreated { get; set; }
        public DateTime? Deadline { get; set; }
    }
}

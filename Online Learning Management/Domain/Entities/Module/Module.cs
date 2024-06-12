using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Management.Domain.Entities.Module
{
    public class Module
    {
        [Key]
        public Guid Id { get; set; }
        public string CourseID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

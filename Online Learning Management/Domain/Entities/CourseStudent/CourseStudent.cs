using System.Data;

namespace Online_Learning_Management.Domain.Entities.CourseStudent
{
    public class CourseStudent
    {
        public Guid Id { get; set; }
        public Guid CourseID { get; set; }
        public Guid StudentID { get; set; }
        public DateTime EnrollmentDate{get;set;} = DateTime.UtcNow;
        public decimal Progress { get; set; }
       
       
    }
}

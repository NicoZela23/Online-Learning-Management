namespace Online_Learning_Management.Domain.Entities.GradeStudents
{
    public class GradeStudents
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public decimal Score { get; set; }
        public string Description { get; set; }
    }
}

namespace Online_Learning_Management.Domain.Entities.GradeStudents
{
    public class GradeStudents
    {
        public Guid Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public decimal Score { get; set; }
        public string Description { get; set; }
    }
}

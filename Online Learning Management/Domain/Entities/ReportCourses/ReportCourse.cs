namespace Online_Learning_Management.Domain.Entities.ReportCourses
{
    public class ReportCourse
    {
        public Guid Id { get; set; }
        public Guid StudentID { get; set; }
        public Guid CourseID { get; set; }
        public string StudentName { get; set; }
        public string CourseName { get; set; }
        public double ProgressPercentage { get; set; }
    }
}

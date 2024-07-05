namespace Online_Learning_Management.Domain.Exceptions.Auth
{
    public class InstructorNotFoundException : Exception
    {
        public InstructorNotFoundException() : base("Instructor not found") { }
    }
}

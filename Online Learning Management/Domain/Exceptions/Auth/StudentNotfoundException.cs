namespace Online_Learning_Management.Domain.Exceptions.Auth
{
    public class StudentNotfoundException : Exception
    {
        public StudentNotfoundException() :base("Student not found") { }
    }
}

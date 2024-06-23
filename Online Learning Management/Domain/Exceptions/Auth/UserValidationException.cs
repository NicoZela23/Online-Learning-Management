namespace Online_Learning_Management.Domain.Exceptions.Auth
{
    public class UserValidationException : Exception
    {
        public UserValidationException(string message) : base(message) { }
    }
}

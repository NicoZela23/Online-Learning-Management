namespace Online_Learning_Management.Domain.Exceptions.Auth
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() :base("User not found") { }
    }
}

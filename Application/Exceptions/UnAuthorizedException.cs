namespace Application.Exceptions
{
    public class UnAuthorizedException : Exception
    {
        public UnAuthorizedException() : base("You are unauthorized to view this resource")
        {
            
        }
        public UnAuthorizedException(string message) : base(message)
        {
            
        }
    }
}

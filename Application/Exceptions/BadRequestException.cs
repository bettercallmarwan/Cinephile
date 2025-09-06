namespace Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string mesage)  : base(mesage)
        {
            
        }
    }
}

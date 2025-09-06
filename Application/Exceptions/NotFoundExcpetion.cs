namespace Application.Exceptions
{
    public class NotFoundExcpetion : Exception
    {
        public NotFoundExcpetion() : base("This entity is not found"){}
        public NotFoundExcpetion(string message) : base(message){}
        public NotFoundExcpetion(string EntityName, int id) : base($"{EntityName} with Id : {id} is not found")
        {
            
        }
    }
}

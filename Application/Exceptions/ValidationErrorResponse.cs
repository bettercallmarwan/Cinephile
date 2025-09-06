using System.Text;
using System.Text.Json;

namespace Application.Exceptions
{
    public class ValidationErrorResponse : Exception
    {
        public Dictionary<string, string[]> Errors { get; set; } = new();

        public ValidationErrorResponse(Dictionary<string, string[]> errors)
            : base("One or more validation errors occurred")
        {
            Errors = errors;
        }

        public ValidationErrorResponse(string message) : base(message)
        {
        }

        public ValidationErrorResponse() : base("One or more validation errors occurred")
        {
        }

        public override string ToString()
        {
            var responseObject = new
            {
                StatusCode = 400,
                Message = "Error Validating" ?? base.Message,
                Errors
            };

            return JsonSerializer.Serialize(responseObject, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true 
            });
        }
    }
}

using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Cinephile.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                ProblemDetails problemDetails;
                switch (ex)
                {

                    case NotFoundExcpetion:
                        problemDetails = new ProblemDetails()
                        {
                            Status = StatusCodes.Status404NotFound,
                            Title = ex.Message,
                            Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Status/404"
                        };

                        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                        await httpContext.Response.WriteAsJsonAsync(problemDetails);
                        break;
                    case UnAuthorizedException:
                        problemDetails = new ProblemDetails()
                        {
                            Status = StatusCodes.Status401Unauthorized,
                            Title = ex.Message,
                            Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Status/401"
                        };

                        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await httpContext.Response.WriteAsJsonAsync(problemDetails);
                        break;

                    default:
                        problemDetails = new ProblemDetails()
                        {
                            Status = StatusCodes.Status500InternalServerError,
                            Title = ex.Message,
                            Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Status/500"
                        };

                        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        await httpContext.Response.WriteAsJsonAsync(problemDetails);
                        break;
                }
            }
        }
    }
}

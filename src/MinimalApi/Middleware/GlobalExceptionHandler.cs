using Microsoft.AspNetCore.Diagnostics;
using MinimalApi.Models.DTOs;
using System.Net;

namespace MinimalApi.Middleware
{
    public class GlobalExceptionHandle : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandle> _logger;

        public GlobalExceptionHandle(ILogger<GlobalExceptionHandle> logger)
        {
            _logger = logger;
        }
        //where did we get this dependeancy GlobalExceptionHandle?? 
        //.NET has a built-in Dependency Injection (DI) container. 
        //When the app starts up, it automatically registers a logging service.

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            // 1. Log the error for the developer
            _logger.LogError(exception, "An unhandled exception occurred. {Message}", exception.Message);

            // Determine the status code based on the exception type
            var (statusCode, message) = exception switch
            {
                UnauthorizedAccessException => (401, "You are not authorized."),
                KeyNotFoundException => (404, "The requested resource was not found."),
                ArgumentException => (400, "Invalid input provided."),
                _ => (500, "A technical error occurred.") // Default/Fallback
            };

            //2. Prepare the response
            var response = new ErrorResponse(statusCode, message, exception.Message);

            //3. Set the HTTP status and return JSON
            httpContext.Response.StatusCode = response.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            return true; // Indicates we handled the error

        }

    }
}
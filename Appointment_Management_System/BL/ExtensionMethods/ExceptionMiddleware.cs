using Appointment_Management_System.Models;
using Microsoft.AspNetCore.Http;

namespace Appointment_Management_System.BL.ExtensionMethods
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            // Keep default 400 for business errors
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new
            {
                success = false,
                message = ex.Message
            };

            return context.Response.WriteAsJsonAsync(response);

        }
    }
}

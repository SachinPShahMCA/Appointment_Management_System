using Appointment_Management_System.Observability.Telemetry;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Appointment_Management_System.BL.Exceptions;

namespace Appointment_Management_System.Observability.Middlewares
{
    public sealed class ExceptionMiddleware: IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IActionTelemetry _telemetry;

        public ExceptionMiddleware(
            ILogger<ExceptionMiddleware> logger,
            IActionTelemetry telemetry)
        {
            _logger = logger;
            _telemetry = telemetry;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                if (ex is BusinessException)
                {
                    context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                }
                else
                { 
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                if (ex is not BusinessException)
                {
                    _logger.LogError(ex, "Unhandled system exception");

                    _telemetry.Track("UNHANDLED_EXCEPTION", new
                    {
                        context.Request.Path,
                        context.Request.Method
                    });
                }
                }

                var json = JsonSerializer.Serialize(new
                {
                    success = false,
                    message = ex.Message,
                    correlationId =
                        context.Response.Headers["X-Correlation-Id"].ToString()
                });

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }
        }
    }

}

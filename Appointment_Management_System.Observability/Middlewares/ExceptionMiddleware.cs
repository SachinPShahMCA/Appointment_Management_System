using Appointment_Management_System.BL.Exceptions;
using Appointment_Management_System.Observability.Stores;
using Appointment_Management_System.Observability.Telemetry;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Appointment_Management_System.Observability.Middlewares
{
    public sealed class ExceptionMiddleware: IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IActionTelemetry _telemetry;
        private readonly IExceptionStore _exceptionStore;

        public ExceptionMiddleware(
            ILogger<ExceptionMiddleware> logger,
            IActionTelemetry telemetry,
             IExceptionStore exceptionStore)
        {
            _logger = logger;
            _telemetry = telemetry;
            _exceptionStore= exceptionStore;
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
                    var jsonb = JsonSerializer.Serialize(new
                    {
                        success = false,
                        message = ex.Message,
                        correlationId = context.Response.Headers["X-Correlation-Id"].ToString()
                    });
                    await context.Response.WriteAsync(jsonb);
                    return;
                }
                else
                { 
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    _logger.LogError(ex, "Unhandled system exception");
                  await _telemetry.TrackAsync("UNHANDLED_EXCEPTION", new
                    {
                        context.Request.Path,
                        context.Request.Method
                    });
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

                await _exceptionStore.StoreAsync(ex, context);
            }
        }
    }

}

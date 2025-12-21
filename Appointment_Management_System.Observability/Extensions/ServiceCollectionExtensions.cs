using Appointment_Management_System.Common.Log;
using Appointment_Management_System.Observability.Middlewares;
using Appointment_Management_System.Observability.Telemetry;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment_Management_System.Observability.Extensions
{
    public static class ObservabilityExtensions
    {
        public static IServiceCollection AddObservability(this IServiceCollection services)
        {
            services.AddScoped<ILogContextAccessor, LogContextAccessor>();
            services.AddScoped<IActionTelemetry, ActionTelemetry>();

            services.AddScoped<LogContextMiddleware>();
            services.AddScoped<ExceptionMiddleware>();
            services.AddScoped<PerformanceMiddleware>();

            return services;
        }
    }

}

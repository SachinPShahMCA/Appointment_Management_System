using Appointment_Management_System.Observability.Telemetry;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_Management_System.Observability.Middlewares
{
    public sealed class PerformanceMiddleware: IMiddleware
    {
        private const int ThresholdMs = 3000;
        private readonly ILogger<PerformanceMiddleware> _logger;
        private readonly IActionTelemetry _telemetry;

        public PerformanceMiddleware(
            ILogger<PerformanceMiddleware> logger,
            IActionTelemetry telemetry)
        {
            _logger = logger;
            _telemetry = telemetry;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var sw = Stopwatch.StartNew();
            await next(context);
            sw.Stop();

            if (sw.ElapsedMilliseconds > ThresholdMs)
            {
                _logger.LogWarning(
                    "SLOW API | {Path} | {Elapsed}ms",
                    context.Request.Path,
                    sw.ElapsedMilliseconds);

                _telemetry.Track("SLOW_API", new
                {
                    context.Request.Path,
                    context.Request.Method,
                    sw.ElapsedMilliseconds
                });
            }
        }
    }

}

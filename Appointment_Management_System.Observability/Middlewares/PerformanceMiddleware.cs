using Appointment_Management_System.Observability.Stores;
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
        private readonly IPerformanceStore _performanceStore;

        public PerformanceMiddleware(
            ILogger<PerformanceMiddleware> logger,
            IActionTelemetry telemetry,
            IPerformanceStore performanceStore)
        {
            _logger = logger;
            _telemetry = telemetry;
            _performanceStore = performanceStore;
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

                await _telemetry.TrackAsync("SLOW_API", new
                {
                    context.Request.Path,
                    context.Request.Method,
                    sw.ElapsedMilliseconds
                });

                await _performanceStore.StoreAsync(context, sw.ElapsedMilliseconds, ThresholdMs);
            }
        }
    }

}

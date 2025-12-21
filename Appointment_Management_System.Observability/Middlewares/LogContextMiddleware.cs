using Appointment_Management_System.Common.Log;
using Microsoft.AspNetCore.Http;
using AppLogContext = Appointment_Management_System.Common.Log.LogContext;
using SerilogLogContext = Serilog.Context.LogContext;

namespace Appointment_Management_System.Observability.Middlewares
{
    public sealed class LogContextMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var tenantHeader = context.Request.Headers["X-Tenant-Id"].FirstOrDefault();
            int tenantId = 1;
            if (int.TryParse(tenantHeader, out var parsedTenantId) && parsedTenantId > 0)
            {
                tenantId = parsedTenantId;
            }
                       
            var logContext = new AppLogContext
            {
                TenantId = tenantId,
                UserId = context.User?.Identity?.Name ?? "anonymous",
                CorrelationId =
           context.Request.Headers["X-Correlation-Id"].FirstOrDefault()
           ?? Guid.NewGuid().ToString()
            };

            LogContextAccessor.Set(logContext);
            

            using (SerilogLogContext.PushProperty("TenantId", logContext.TenantId))
            using (SerilogLogContext.PushProperty("UserId", logContext.UserId))
            using (SerilogLogContext.PushProperty("CorrelationId", logContext.CorrelationId))
            {
                // 5️⃣ Return CorrelationId to client
                context.Response.Headers["X-Correlation-Id"] = logContext.CorrelationId;

                // 6️⃣ Continue pipeline
                await next(context);
            }

        }
    }

}

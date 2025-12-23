using Appointment_Management_System.Common.Log;
using Appointment_Management_System.Data;
using Appointment_Management_System.Observability.Entities;

namespace Appointment_Management_System.Observability.Stores
{
    public sealed class PerformanceStore : IPerformanceStore
    {
        private readonly ObservabilityDbContext _db;
        private readonly ILogContextAccessor _ctx;

        public PerformanceStore(
            ObservabilityDbContext db,
            ILogContextAccessor ctx)
        {
            _db = db;
            _ctx = ctx;
        }

        public async Task StoreAsync(HttpContext context, long elapsedMs, int thresholdMs)
        {
            try
            {
                var c = _ctx.Current!;

                _db.PerformanceLogs.Add(new PerformanceLog
                {
                    TenantId = c.TenantId,
                    UserId = c.UserId,
                    CorrelationId = Guid.Parse(c.CorrelationId),
                    HttpMethod = context.Request.Method,
                    Path = context.Request.Path,
                    ElapsedMilliseconds = (int)elapsedMs,
                    ThresholdMilliseconds = thresholdMs
                });

                await _db.SaveChangesAsync();
            }
            catch { }
        }
    }

}

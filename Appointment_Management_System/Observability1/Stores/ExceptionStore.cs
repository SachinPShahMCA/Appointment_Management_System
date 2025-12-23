using Appointment_Management_System.Common.Log;
using Appointment_Management_System.Observability.Entities;
using Appointment_Management_System.Data;

namespace Appointment_Management_System.Observability.Stores
{
    public sealed class ExceptionStore : IExceptionStore
    {
        private readonly ObservabilityDbContext _db;
        private readonly ILogContextAccessor _ctx;

        public ExceptionStore(
            ObservabilityDbContext db,
            ILogContextAccessor ctx)
        {
            _db = db;
            _ctx = ctx;
        }

        public async Task StoreAsync(Exception ex, HttpContext context)
        {
            try
            {
                var c = _ctx.Current!;

                _db.SystemExceptionLogs.Add(new SystemExceptionLog
                {
                    TenantId = c.TenantId,
                    UserId = c.UserId,
                    CorrelationId = Guid.Parse(c.CorrelationId),

                    HttpMethod = context.Request.Method,
                    Path = context.Request.Path,

                    ExceptionType = ex.GetType().FullName!,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                });

                await _db.SaveChangesAsync();
            }
            catch
            {
                // MUST NEVER break production
            }
        }
    }

}

using Appointment_Management_System.Common.Log;
using Appointment_Management_System.Data;
using Appointment_Management_System.Observability.Entities;
using System.Text.Json;

namespace Appointment_Management_System.Observability.Stores
{
    public sealed class TelemetryStore : ITelemetryStore
    {
        private readonly ObservabilityDbContext _db;
        private readonly ILogContextAccessor _ctx;

        public TelemetryStore(
            ObservabilityDbContext db,
            ILogContextAccessor ctx)
        {
            _db = db;
            _ctx = ctx;
        }

        public async Task StoreAsync(string action, object? data)
        {
            try
            {
                var c = _ctx.Current!;

                _db.TelemetryEvents.Add(new TelemetryEvent
                {
                    TenantId = c.TenantId,
                    UserId = c.UserId,
                    CorrelationId = Guid.Parse(c.CorrelationId),
                    EventName = action,
                    Payload = data == null
                        ? null
                        : JsonSerializer.Serialize(data)
                });

                await _db.SaveChangesAsync();
            }
            catch { }
        }
    }

}

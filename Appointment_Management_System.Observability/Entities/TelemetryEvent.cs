using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_Management_System.Observability.Entities
{
    public sealed class TelemetryEvent
    {
        public long Id { get; set; }
        public DateTime OccurredAtUtc { get; set; } = DateTime.UtcNow;

        public int TenantId { get; set; }
        public string? UserId { get; set; }
        public Guid CorrelationId { get; set; }

        public string EventName { get; set; } = null!;
        public string? Payload { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_Management_System.Observability.Entities
{
    public sealed class SystemExceptionLog
    {
        public long Id { get; set; }
        public DateTime OccurredAtUtc { get; set; }

        public int TenantId { get; set; }
        public string? UserId { get; set; }
        public Guid CorrelationId { get; set; }

        public string HttpMethod { get; set; } = null!;
        public string Path { get; set; } = null!;

        public string ExceptionType { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string? StackTrace { get; set; }
    }

}

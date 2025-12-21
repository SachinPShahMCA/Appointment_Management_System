using Appointment_Management_System.Common.Log;
using Microsoft.Extensions.Logging;

namespace Appointment_Management_System.Observability.Telemetry
{
    public sealed class ActionTelemetry : IActionTelemetry
    {
        private readonly ILogger<ActionTelemetry> _logger;
        private readonly ILogContextAccessor _ctx;

        public ActionTelemetry(
            ILogger<ActionTelemetry> logger,
            ILogContextAccessor ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }

        public void Track(string action, object data = null)
        {
            var c = _ctx.Current;

            _logger.LogInformation(
                "ACTION | {Action} | Tenant:{Tenant} | User:{User} | Correlation:{Correlation} | Data:{Data}",
                action,
                c?.TenantId,
                c?.UserId,
                c?.CorrelationId,
                data);
        }
    }

}

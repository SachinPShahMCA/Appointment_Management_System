using Appointment_Management_System.Common.Log;
using Appointment_Management_System.Observability.Stores;
using Microsoft.Extensions.Logging;

namespace Appointment_Management_System.Observability.Telemetry
{
    public sealed class ActionTelemetry : IActionTelemetry
    {
        private readonly ILogger<ActionTelemetry> _logger;
        private readonly ILogContextAccessor _ctx;
        private readonly ITelemetryStore _telemetryStore;

        public ActionTelemetry(
            ILogger<ActionTelemetry> logger,
            ILogContextAccessor ctx,
            ITelemetryStore telemetryStore)
        {
            _logger = logger;
            _ctx = ctx;
            _telemetryStore = telemetryStore;
        }

        public async Task TrackAsync(string action, object data = null)
        {
            var c = _ctx.Current;

            _logger.LogInformation(
                "ACTION | {Action} | Tenant:{Tenant} | User:{User} | Correlation:{Correlation} | Data:{Data}",
                action,
                c?.TenantId,
                c?.UserId,
                c?.CorrelationId,
                data);
            await _telemetryStore.StoreAsync(action, data);
        }
    }

}

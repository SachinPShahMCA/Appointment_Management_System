namespace Appointment_Management_System.Common.Log
{
    public sealed class LogContext
    {
        public int TenantId { get; init; } = 1;
        public string UserId { get; init; } = "1";
        public required string CorrelationId { get; init; }
    }
}

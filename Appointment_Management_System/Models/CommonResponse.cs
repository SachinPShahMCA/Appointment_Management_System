namespace Appointment_Management_System.Models
{
    public class CommonResponse
    {
        public bool Success { get; init; }
        public int StatusCode { get; init; }
        public string? Message { get; init; }
        public object? Data { get; init; }
        public string? Path { get; init; }
        public DateTime Timestamp { get; init; } = DateTime.UtcNow;
        public string? CorrelationId { get; init; }
    }

}

namespace Appointment_Management_System.Models
{
    public class CommonResponse
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
        public string? Path { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}

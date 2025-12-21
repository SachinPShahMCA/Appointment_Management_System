namespace Appointment_Management_System.Common.Log
{
    public sealed class LogContextAccessor : ILogContextAccessor
    {
        private static readonly AsyncLocal<LogContext> _current = new();

        public LogContext Current => _current.Value;

        public static void Set(LogContext context)
            => _current.Value = context;
    }

}

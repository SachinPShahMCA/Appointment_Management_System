namespace Appointment_Management_System.Common.Log
{
    public interface ILogContextAccessor
    {
        LogContext Current { get; }
    }
}

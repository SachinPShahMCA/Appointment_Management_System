namespace Appointment_Management_System.BL.Exceptions
{
    public sealed class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }
    }
}

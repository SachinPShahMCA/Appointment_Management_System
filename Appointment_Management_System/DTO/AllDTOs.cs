namespace Appointment_Management_System.DTO
{
    public class AppointmentDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string Subject { get; set; }
        public string? Description { get; set; }
        public string? DoctorComment { get; set; }
    }
    public class AppointmentValidationResult
    {
        public bool IsValid { get; set; }
        public string? Message { get; set; }
    }

    public class PatientDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";

        public string Mobile { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }

    public class PatientCreateUpdateDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string Mobile { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public bool isActive { get; set; }
    }

    public class PatientDropdownDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }
    public class DoctorDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";

        public string Mobile { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string? Gender { get; set; }
    }

    public class DoctorCreateUpdateDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string? Gender { get; set; }

        public string Mobile { get; set; } = string.Empty;
        public string? Email { get; set; }
    }

    public class DoctorDropdownDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }

}

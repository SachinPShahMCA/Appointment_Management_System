using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Appointment_Management_System.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }


        // Auditing
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }
    }
    public class Appointment : BaseEntity
    {
        public int PatientId { get; set; }
        public Patient patient { get; set; }

        public int DoctorId { get; set; }
        public Doctor doctor { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public required string Subject { get; set; }
        public string? Description { get; set; }
        public string? DoctorComment { get; set; }
    }
    public class Patient : BaseEntity
    {
       
        [StringLength(150)]
        public required string FirstName { get; set; }

        
        [StringLength(150)]
        public required string LastName { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
                
        [StringLength(15)]
        public required string Mobile { get; set; }

        [StringLength(150)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        
        public DateTime DateOfBirth { get; set; }

        [StringLength(1)]
        public string Gender { get; set; }  // Male, Female, Other

        
        public bool IsActive { get; set; } = true;

        // Relationship (Patient can have multiple appointments)
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
    public class Doctor : BaseEntity
    {
        // ---------------------------
        //  BASIC PROFILE INFORMATION
        // ---------------------------
        [Required]
        [StringLength(100)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public required string LastName { get; set; }
        [MaxLength(1)]
        public string? Gender { get; set; } // OR enum (optional)

        [MaxLength(10)]
        public string Mobile { get; set; } = string.Empty;

        [StringLength(150)]
        [EmailAddress]
        public string? Email { get; set; }

                 // ---------------------------
        public ICollection<Appointment> Appointments { get; set; }
            = new List<Appointment>(); // EF-safe initialization

        // ---------------------------
        //  CALCULATED PROPERTIES
        // ---------------------------
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

    }


}

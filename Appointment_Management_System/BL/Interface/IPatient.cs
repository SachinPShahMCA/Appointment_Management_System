using Appointment_Management_System.DTO;
using Appointment_Management_System.Models;

namespace Appointment_Management_System.BL.Interface
{
    public interface IPatient: IGenericRepository<Patient>
    {
        Task<List<PatientDropdownDto>> GetDropdownAsync();       
    }
}

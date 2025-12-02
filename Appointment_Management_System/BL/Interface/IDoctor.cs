using Appointment_Management_System.DTO;
using Appointment_Management_System.Models;

namespace Appointment_Management_System.BL.Interface
{
    public interface IDoctor:IGenericRepository<Doctor>
    {
        Task<List<DoctorDropdownDto>> GetDropdownAsync();

    }
}

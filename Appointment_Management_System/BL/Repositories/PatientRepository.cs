using Appointment_Management_System.BL.Exceptions;
using Appointment_Management_System.BL.Interface;
using Appointment_Management_System.Data;
using Appointment_Management_System.DTO;
using Appointment_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Appointment_Management_System.BL.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatient
    {
        public PatientRepository(AppDbContext context) : base(context)
        {

        }

        public override async Task Delete(int id)
        {
            var Patient = await GetByIdAsync(id);
            if (Patient == null)
                throw new Exception("Patient not found");

            // Check appointments
            bool hasAppointments = await _context.Appointments
                .AnyAsync(a => a.PatientId == id);

            if (hasAppointments)
                throw new BusinessException("Patient cannot be deleted because appointments exist.");

            // Soft delete
            Patient.IsDeleted = true;
            _context.Entry(Patient).State = EntityState.Modified;

            await SaveAsync();
        }

        public async Task<List<PatientDropdownDto>> GetDropdownAsync()
        {
            return await _context.Patients
                .Where(p => p.IsActive)            // global filter handles !IsDeleted
                .Select(p => new PatientDropdownDto
                {
                    Id = p.Id,
                    FullName = p.FirstName + " " + p.LastName
                })
                .OrderBy(p => p.FullName)
                .ToListAsync();
        }


    }
}

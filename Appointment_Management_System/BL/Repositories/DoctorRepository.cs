using Appointment_Management_System.BL.Interface;
using Appointment_Management_System.Data;
using Appointment_Management_System.DTO;
using Appointment_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Appointment_Management_System.BL.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctor
    {
        public DoctorRepository(AppDbContext context) : base(context)
        {
        }
        public override async Task Delete(int id)
        {
            var doctor = await GetByIdAsync(id);
            if (doctor == null)
                throw new Exception("Doctor not found");

            // Check if doctor has any appointments
            bool hasAppointments = await _context.Appointments
                .AnyAsync(a => a.DoctorId == id);

            if (hasAppointments)
                throw new Exception("Doctor cannot be deleted because appointments exist.");

            // Soft delete
            doctor.IsDeleted = true;
            _context.Entry(doctor).State = EntityState.Modified;

            await SaveAsync();
        }

        public async Task<List<DoctorDropdownDto>> GetDropdownAsync()
        {
            return await _context.Doctors
                .Select(d => new DoctorDropdownDto
                {
                    Id = d.Id,
                    FullName = d.FirstName + " " + d.LastName
                })
                .OrderBy(d => d.FullName)
                .ToListAsync();
        }

    }
}

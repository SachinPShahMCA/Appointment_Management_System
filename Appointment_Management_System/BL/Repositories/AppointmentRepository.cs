using Appointment_Management_System.BL.ExtensionMethods;
using Appointment_Management_System.BL.Interface;
using Appointment_Management_System.Data;
using Appointment_Management_System.DTO;
using Appointment_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Appointment_Management_System.BL.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointment
    {
        protected readonly AppDbContext _context;

        public AppointmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public AppointmentValidationResult ValidateAppointment(AppointmentDto dto)
        {
            
            if (string.IsNullOrWhiteSpace(dto.Subject) || dto.Subject.Length < 3 || dto.Subject.Length > 100)
                return new AppointmentValidationResult
                {
                    IsValid = false,
                    Message = "Subject must be between 3 and 100 characters."
                };

            
            TimeSpan officeStart = new(4,30, 0);
            TimeSpan officeEnd = new(11, 30, 0);

            if (dto.StartTime.TimeOfDay < officeStart || dto.EndTime.TimeOfDay > officeEnd)
                return new AppointmentValidationResult
                {
                    IsValid = false,
                    Message = "Appointment must be between 10:00 AM and 05:00 PM."
                };

            if ((dto.EndTime - (dto.StartTime)).TotalHours > 1)
                return new AppointmentValidationResult
                {
                    IsValid = false,
                    Message = "Appointment duration must be exactly 1 hour"
                };
            
            bool isOverlap = _context.Appointments.Any(a => a.DoctorId == dto.DoctorId &&
                                                                 dto.StartTime < a.EndTime &&
                                                                   dto.EndTime > a.StartTime);
            if (isOverlap)
                return new AppointmentValidationResult
                {
                    IsValid = false,
                    Message = "This time slot is already booked."
                };

            return new AppointmentValidationResult
            {
                IsValid = true,
                Message = "Appointment is valid."
            };
        }

        public override async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _context.Appointments
                .Include(a => a.patient)
                .Include(a => a.doctor)
                .ToListAsync();
        }
        public override async Task<Appointment?> GetByIdAsync(int id)
        {
            return await _context.Appointments
                .Include(a => a.patient)
                .Include(a => a.doctor)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

    }
}

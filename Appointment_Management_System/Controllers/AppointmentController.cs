using Appointment_Management_System.BL.ExtensionMethods;
using Appointment_Management_System.BL.Interface;
using Appointment_Management_System.BL.Repositories;
using Appointment_Management_System.DTO;
using Appointment_Management_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appointment_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointment _appointmentRepository;
        public AppointmentController(IAppointment appointmenRepository)
        {
            _appointmentRepository = appointmenRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await _appointmentRepository.GetAllAsync();

            if (appointments == null || !appointments.Any())
                return NotFound("No appointments found");
            //else {
            //    foreach (var ap in appointments)
            //    {
            //        ap.StartTime = DateTime.SpecifyKind(ap.StartTime, DateTimeKind.Utc);
            //        ap.EndTime = DateTime.SpecifyKind(ap.EndTime, DateTimeKind.Utc);
            //    }
            //}

                return Ok(appointments);
        }
        [HttpGet("{id:int}")]

        public async Task<IActionResult> GetAppointmentById(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            if (appointment == null)
                return NotFound($"Appointment with ID {id} not found");

            return Ok(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var validation = _appointmentRepository.ValidateAppointment(dto);

            if (!validation.IsValid)
                return BadRequest(validation.Message);

            Appointment appointment = new Appointment
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Subject = dto.Subject,
                Description = dto.Description,
                DoctorComment = dto.DoctorComment
            };

            await _appointmentRepository.AddAsync(appointment);
            await _appointmentRepository.SaveAsync();

            return Created("/api/appointment/" + appointment.Id, appointment);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AppointmentDto dto)
        {
            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);

            var appointment = await _appointmentRepository.GetByIdAsync(id);

            if (appointment == null)
                return NotFound($"Appointment with ID {id} not found");

            // Map updates
            appointment.PatientId = dto.PatientId;
            appointment.DoctorId = dto.DoctorId;
            appointment.StartTime = dto.StartTime;
            appointment.EndTime = dto.EndTime;
            appointment.Subject = dto.Subject;
            appointment.Description = dto.Description;
            appointment.DoctorComment = dto.DoctorComment;

            _appointmentRepository.Update(appointment);
            await _appointmentRepository.SaveAsync();

            return Ok($"Appointment {id} updated successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            if (appointment == null)
                return NotFound($"Appointment with ID {id} not found");

            await _appointmentRepository.Delete(appointment.Id);
            await _appointmentRepository.SaveAsync();

            return Ok($"Appointment {id} Canceled successfully");
        }
    }
}

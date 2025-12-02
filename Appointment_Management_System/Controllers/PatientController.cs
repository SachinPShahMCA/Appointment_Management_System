using Appointment_Management_System.BL.Interface;
using Appointment_Management_System.DTO;
using Appointment_Management_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Appointment_Management_System.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatient _patientRepository;

        public PatientController(IPatient patientRepository)
        {
            _patientRepository = patientRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] PatientCreateUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Patient patient = new Patient
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Gender = dto.Gender,
                Mobile = dto.Mobile,
                Email = dto.Email
            };

            await _patientRepository.AddAsync(patient);
            await _patientRepository.SaveAsync();

            return Created("/api/patient/" + patient.Id, patient);
        }

       
        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            var Response = await _patientRepository.GetAllAsync();
            if (!Response.Any()) return NotFound();
            return Ok(Response);
        }
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetPatientDropdown()
        {
            var result = await _patientRepository.GetDropdownAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            if (id == 0)
                return BadRequest("Invalid Id");

            var patient = await _patientRepository.GetByIdAsync(id);

            if (patient == null)
                return NotFound("Patient not found");

            return Ok(patient);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            if (id == 0)
                return BadRequest("Invalid Id");

            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null)
                return NotFound("Patient not found");

            await _patientRepository.Delete(id);
           

            return Ok(await _patientRepository.SaveAsync());
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] PatientCreateUpdateDto dto)
        {
            if (id == 0)
                return BadRequest("Invalid Id");

            var record = await _patientRepository.GetByIdAsync(id);
            if (record == null)
                return NotFound("Patient not found");

            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);

            // ---- Local mapper ----
            record.FirstName = dto.FirstName;
            record.DateOfBirth = dto.DateOfBirth;
            record.IsActive = dto.isActive;
            record.LastName = dto.LastName;
            record.Gender = dto.Gender;
            record.Mobile = dto.Mobile;
            record.Email = dto.Email;

            _patientRepository.Update(record);
            

            return Ok(await _patientRepository.SaveAsync());
        }

        
        private static PatientCreateUpdateDto MapPatientToDto(Patient p)
        {
            return new PatientCreateUpdateDto
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                Gender = p.Gender,
                Mobile = p.Mobile,
                Email = p.Email
            };
        }

    }
}

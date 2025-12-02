using Appointment_Management_System.BL.Interface;
using Appointment_Management_System.DTO;
using Appointment_Management_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Reflection;

namespace Appointment_Management_System.Controllers
{
    [Route("api/doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctor _doctorRepository;
        public DoctorController(IDoctor doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorCreateUpdateDto dto)
        {  
            // ---------- Manual Mapping ----------
            Doctor doctor = new Doctor
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Gender = dto.Gender,
                Mobile = dto.Mobile,
                Email = dto.Email
            };
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await  _doctorRepository.AddAsync(doctor);
            await _doctorRepository.SaveAsync(); 
            return Created("/api/doctor/" + doctor.Id, doctor);
            // return CreatedAtAction(nameof(GetDoctorById), new { id = doctor.Id }, doctor);
        }
        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {
            var response = await _doctorRepository.GetAllAsync();
            if (!response.Any()) return NotFound();
            return Ok(response);
        }
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDoctorDropdown()
        {
            var result = await _doctorRepository.GetDropdownAsync();
            return Ok(result);
        }


        [HttpGet("{id:int}")]        
        public async Task<IActionResult> GetDoctorById(int id)
        {
            if (id == 0) return BadRequest();
            var response = await _doctorRepository.GetByIdAsync(id);
            if (response == null) return NotFound();
            else return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctorById(int id)
        {
            await _doctorRepository.Delete(id);
            return Ok(await _doctorRepository.SaveAsync());
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id,[FromBody] DoctorCreateUpdateDto dto)
        {
            if (id==0)
                return BadRequest(ModelState);

            var Record = await _doctorRepository.GetByIdAsync(id);

            if (Record is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);

            Record.FirstName = dto.FirstName;
            Record.LastName = dto.LastName;
            Record.Gender = dto.Gender;
            Record.Mobile = dto.Mobile;
            Record.Email = dto.Email;


             _doctorRepository.Update(Record);
            await _doctorRepository.SaveAsync();
            return Ok(await _doctorRepository.SaveAsync());
        }
        private static DoctorCreateUpdateDto MapDoctorToDto(Doctor doctor)
        {
            return new DoctorCreateUpdateDto
            {
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Gender = doctor.Gender,
                Mobile = doctor.Mobile,
                Email = doctor.Email
            };
        }
    }
}

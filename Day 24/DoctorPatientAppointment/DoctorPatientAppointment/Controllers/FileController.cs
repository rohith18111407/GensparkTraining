using System.Text.Json;
using DoctorPatientAppointment.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DoctorPatientAppointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly string filePath = "patients.json";

        [HttpGet]
        public IActionResult GetAllPatients()
        {
            if (!System.IO.File.Exists(filePath))
            {
                return Ok(new List<PatientFileDto>());
            }

            var json = System.IO.File.ReadAllText(filePath);
            var patients = JsonSerializer.Deserialize<List<PatientFileDto>>(json) ?? new List<PatientFileDto>();
            return Ok(patients);
        }

        [HttpPost]
        public async Task<IActionResult> AddPatient([FromBody] PatientFileDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<PatientFileDto> patients = new();

            if (System.IO.File.Exists(filePath))
            {
                var json = await System.IO.File.ReadAllTextAsync(filePath);
                patients = JsonSerializer.Deserialize<List<PatientFileDto>>(json) ?? new();
            }

            var newPatient = new PatientFileDto
            {
                HospitalName = "Doctor Patient Appointment Hospitals",
                CreatedAt = DateTime.Now,
                PatientName = input.PatientName,
                PatientMobile = input.PatientMobile,
                PatientAddress = input.PatientAddress,
                PatientAge = input.PatientAge
            };

            patients.Add(newPatient);

            var updatedJson = JsonSerializer.Serialize(patients, new JsonSerializerOptions { WriteIndented = true });
            await System.IO.File.WriteAllTextAsync(filePath, updatedJson);

            return Ok("Patient data added successfully.");
        }
    }
}
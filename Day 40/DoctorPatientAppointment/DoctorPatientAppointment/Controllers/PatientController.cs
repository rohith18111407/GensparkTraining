using DoctorPatientAppointment.Interfaces;
using DoctorPatientAppointment.Models;
using DoctorPatientAppointment.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorPatientAppointment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        
        [HttpPost]
        public async Task<ActionResult<Patient>> AddPatient([FromBody] PatientAddRequestDto patientDto)
        {
            try
            {
                var addedPatient = await _patientService.AddPatient(patientDto);
                return Created("Added the patient successfully", addedPatient);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("{id}")]
        
        public async Task<ActionResult<Patient>> GetPatientById(int id)
        {
            try
            {
                var patient = await _patientService.GetPatientById(id);
                return Ok(patient);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetAllPatients()
        {
            try
            {
                var patients = await _patientService.GetAllPatients();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}

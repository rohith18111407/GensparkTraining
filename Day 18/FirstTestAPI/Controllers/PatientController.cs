using FirstTestAPI.Interfaces;
using FirstTestAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstTestAPI.Controllers
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetById(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null)
                return NotFound("Patient not found");
            return Ok(patient);
        }

        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient([FromBody] Patient patient)
        {
            var createdPatient = await _patientService.AddPatientAsync(patient);
            if (createdPatient == null)
                return BadRequest("Doctor not found or patient could not be added");

            return CreatedAtAction(nameof(GetById), new { id = createdPatient.Id }, createdPatient);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Patient>> DeletePatient(int id)
        {
            var deleted = await _patientService.DeletePatientAsync(id);
            if (deleted == null)
                return NotFound("Patient not found");
            return Ok(deleted);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Patient>> UpdatePatient(int id, [FromBody] Patient updatedPatient)
        {
            if (id != updatedPatient.Id)
                return BadRequest("ID mismatch");

            var result = await _patientService.UpdatePatientAsync(updatedPatient);
            if (result == null)
                return NotFound("Patient or Doctor not found");

            return Ok(result);
        }
    }
}

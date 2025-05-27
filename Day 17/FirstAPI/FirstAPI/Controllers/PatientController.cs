using FirstAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PatientController : ControllerBase
    {
        static List<Patient> patients = new List<Patient>
        {
            new Patient{
                Id = 101,
                Name = "Travis",
                Age = 32,
                Address = "No 32A, ABC Colony, Australia",
                Reason = "Headache"
            },
            new Patient{
                Id = 102,
                Name = "Andreew",
                Age = 32,
                Address = "No 321, CBA Colony, West Indies",
                Reason = "Extreme anger"
            },
            new Patient{
                Id = 103,
                Name = "Kieron",
                Age = 32,
                Address = "102A/42/5, CAB Colony, West Indies",
                Reason = "Hand Injury"
            }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Patient>> GetPatients()
        {
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public ActionResult<Patient> GetPatient(int id)
        {
            var findPatient = patients.FirstOrDefault(x => x.Id == id);
            if (findPatient == null)
            {
                return NotFound();
            }
            return Ok(findPatient);
        }

        [HttpPost]
        public ActionResult<Patient> AddPatient([FromBody] Patient newPatient)
        {
            var existingPatient = patients.FirstOrDefault(p => p.Id == newPatient.Id);
            if (existingPatient != null)
            {
                return Conflict($"A patient with Id {newPatient.Id} already exists.");
            }
            patients.Add(newPatient);
            return Created("", newPatient);
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePatient(int id)
        {
            var findPatient = patients.FirstOrDefault(x => x.Id == id);
            if (findPatient == null)
            {
                return NotFound();
            }
            else
            {
                patients.Remove(findPatient);
                return NoContent();
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Patient> UpdatePatient(int id, [FromBody] Patient updatedPatient)
        {
            var existingPatient = patients.FirstOrDefault(p => p.Id == id);
            if (existingPatient == null)
            {
                return NotFound();
            }
            if (updatedPatient.Id != id)
            {
                return BadRequest("Patient ID in body does not match route.");
            }
            existingPatient.Name = updatedPatient.Name;
            existingPatient.Age = updatedPatient.Age;
            existingPatient.Address = updatedPatient.Address;
            existingPatient.Reason = updatedPatient.Reason;

            return Ok(updatedPatient);
        }
    }
}

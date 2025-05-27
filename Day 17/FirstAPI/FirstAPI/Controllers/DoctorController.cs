using FirstAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class DoctorController : ControllerBase
    {
        static List<Doctor> doctors = new List<Doctor>
        {
            new Doctor{
                Id = 101,
                Name = "Rohith"
            },
            new Doctor{
                Id = 102,
                Name = "Mohith"
            }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Doctor>> GetDoctors()
        {
            return Ok(doctors);
        }

        [HttpPost]
        public ActionResult<Doctor> PostDoctor([FromBody] Doctor doctor)
        {
            var existingDoctor = doctors.FirstOrDefault(d => d.Id == doctor.Id);
            if (existingDoctor != null)
            {
                return Conflict($"A doctor with Id {doctor.Id} already exists.");
            }
            doctors.Add(doctor);
            return Created("",doctor);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteDoctor(int id)
        {
            var findDoctor = doctors.FirstOrDefault(x => x.Id == id);
            if (findDoctor == null)
            {
                return NotFound($"Doctor with Id {id} not found");
            }
            else
            {
                doctors.Remove(findDoctor);
                return NoContent();
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Doctor> PutDoctor(int id, [FromBody] Doctor updatedDoctor)
        {
            var existingDoctor = doctors.FirstOrDefault(x => x.Id == id);
            if (existingDoctor == null)
            {
                return NotFound($"Doctor with Id {id} not found");
            }
            if (updatedDoctor.Id != id)
            {
                return BadRequest("Doctor ID in body does not match route.");
            }

            existingDoctor.Name =  updatedDoctor.Name;
            return Ok(existingDoctor);
        }
    }
}

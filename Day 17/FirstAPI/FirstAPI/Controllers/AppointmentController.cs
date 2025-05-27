using FirstAPI.Interfaces;
using FirstAPI.Models;
using FirstAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _repository;

        public AppointmentController(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Appointment>> GetAllAppointments()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Appointment> GetAppointment(int id)
        {
            var appointment = _repository.GetById(id);
            if (appointment == null)
                return NotFound($"Appointment with Id {id} not found.");
            return Ok(appointment);
        }

        [HttpPost]
        public ActionResult<Appointment> CreateAppointment([FromBody] Appointment appointment)
        {
            try
            {
                appointment.Id = 0;
                if (appointment.AppointmentDate <= DateTime.Now)
                {
                    return BadRequest("Appointment must be scheduled for a future date and time.");
                }
                _repository.Add(appointment);
                return Created("", appointment);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Appointment> UpdateAppointment(int id, [FromBody] Appointment updated)
        {
            var existing = _repository.GetById(id);
            if (existing == null)
                return NotFound($"Appointment with Id {id} not found.");

            if (updated.Id != id)
                return BadRequest("Appointment ID cannot be changed.");
            if (updated.AppointmentDate <= DateTime.Now)
            {
                return BadRequest("Appointment must be scheduled for a future date and time.");
            }

            _repository.Update(updated);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAppointment(int id)
        {
            var existing = _repository.GetById(id);
            if (existing == null)
                return NotFound($"Appointment with Id {id} not found.");

            _repository.Delete(id);
            return NoContent();
        }


    }
}

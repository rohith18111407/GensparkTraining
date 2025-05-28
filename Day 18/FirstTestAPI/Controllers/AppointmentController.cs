using FirstTestAPI.Interfaces;
using FirstTestAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstTestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAll()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetById(int id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
                return NotFound("Appointment not found");
            return Ok(appointment);
        }

        [HttpPost]
        public async Task<ActionResult<Appointment>> Post([FromBody] Appointment appointment)
        {
            var created = await _appointmentService.AddAppointmentAsync(appointment);
            if (created == null)
                return BadRequest("Appointment creation failed");

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Appointment>> Put(int id, [FromBody] Appointment appointment)
        {
            if (id != appointment.Id)
                return BadRequest("ID mismatch");

            var updated = await _appointmentService.UpdateAppointmentAsync(appointment);
            if (updated == null)
                return NotFound("Appointment not found");

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Appointment>> Delete(int id)
        {
            var deleted = await _appointmentService.DeleteAppointmentAsync(id);
            if (deleted == null)
                return NotFound("Appointment not found");

            return Ok(deleted);
        }
    }
}

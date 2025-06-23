using System.Security.Claims;
using DoctorPatientAppointment.Interfaces;
using DoctorPatientAppointment.Models;
using DoctorPatientAppointment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorPatientAppointment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmnetService _service;

        public AppointmentController(IAppointmnetService service)
        {
            _service = (AppointmnetService?)service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointmnet>>> GetAll()
        {
            var appointments = await _service.GetAllAppointments();
            if (!appointments.Any())
                return NotFound("No appointments found");
            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Appointmnet>> Get(string id)
        {
            var result = await _service.GetAppointment(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Appointmnet>> Post(Appointmnet appointment)
        {
            var result = await _service.AddAppointment(appointment);
            return CreatedAtAction(nameof(Get), new { id = result.AppointmnetNumber }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Appointmnet>> Put(string id, Appointmnet appointment)
        {
            var result = await _service.UpdateAppointment(id, appointment);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Doctor")]
        [Authorize(Policy = "ExperiencedDoctorOnly")]
        public async Task<ActionResult<Appointmnet>> Delete(string id)
        {
            var result = await _service.DeleteAppointment(id);
            return Ok(result);
        }
    }
}

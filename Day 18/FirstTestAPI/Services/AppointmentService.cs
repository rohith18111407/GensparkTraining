using FirstTestAPI.Interfaces;
using FirstTestAPI.Models;

namespace FirstTestAPI.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Appointment?> AddAppointmentAsync(Appointment appointment)
        {
            return await _appointmentRepository.AddAsync(appointment);
        }

        public async Task<Appointment?> DeleteAppointmentAsync(int id)
        {
            var existing = await _appointmentRepository.GetByIdAsync(id);
            if (existing == null)
                return null;

            await _appointmentRepository.DeleteAsync(id);
            return existing;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _appointmentRepository.GetAllAsync();
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(int id)
        {
            return await _appointmentRepository.GetByIdAsync(id);
        }

        public async Task<Appointment?> UpdateAppointmentAsync(Appointment appointment)
        {
            var existing = await _appointmentRepository.GetByIdAsync(appointment.Id);
            if (existing == null)
                return null;

            existing.PatientId = appointment.PatientId;
            existing.DoctorId = appointment.DoctorId;
            existing.AppointmentDate = appointment.AppointmentDate;
            existing.Reason = appointment.Reason;
            existing.Status = appointment.Status;

            return await _appointmentRepository.UpdateAsync(existing);
        }
    }
}

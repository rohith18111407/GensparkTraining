using DoctorPatientAppointment.Interfaces;
using DoctorPatientAppointment.Models;
using DoctorPatientAppointment.Repositories;

namespace DoctorPatientAppointment.Services
{
    public class AppointmnetService : IAppointmnetService
    {
        private readonly IRepository<string, Appointmnet> _appointmentRepo;

        public AppointmnetService(IRepository<string,Appointmnet> repo)
        {
            _appointmentRepo = repo;
        }

        public async Task<IEnumerable<Appointmnet>> GetAllAppointments()
        {
            return await _appointmentRepo.GetAll();
        }

        public async Task<Appointmnet> GetAppointment(string id)
        {
            return await _appointmentRepo.Get(id);
        }

        public async Task<Appointmnet> AddAppointment(Appointmnet appointment)
        {
            return await _appointmentRepo.Add(appointment);
        }

        public async Task<Appointmnet> UpdateAppointment(string id, Appointmnet appointment)
        {
            return await _appointmentRepo.Update(id, appointment);
        }

        public async Task<Appointmnet> DeleteAppointment(string id)
        {
            return await _appointmentRepo.Delete(id);
        }
    }
}
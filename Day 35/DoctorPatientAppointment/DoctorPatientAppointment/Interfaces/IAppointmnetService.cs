using DoctorPatientAppointment.Models;

namespace DoctorPatientAppointment.Interfaces
{
    public interface IAppointmnetService
    {
        Task<IEnumerable<Appointmnet>> GetAllAppointments();
        Task<Appointmnet> GetAppointment(string id);
        Task<Appointmnet> AddAppointment(Appointmnet appointment);
        Task<Appointmnet> UpdateAppointment(string id, Appointmnet appointment);
        Task<Appointmnet> DeleteAppointment(string id);
    }
}
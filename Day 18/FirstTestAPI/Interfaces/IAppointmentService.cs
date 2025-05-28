using FirstTestAPI.Models;

namespace FirstTestAPI.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment?> GetAppointmentByIdAsync(int id);
        Task<Appointment?> AddAppointmentAsync(Appointment appointment);
        Task<Appointment?> UpdateAppointmentAsync(Appointment appointment);
        Task<Appointment?> DeleteAppointmentAsync(int id);
    }

}

using FirstTestAPI.Contexts;
using FirstTestAPI.Models;
namespace FirstTestAPI.Interfaces
{
    public interface IAppointmentRepository :IRepository<int,Appointment>
    {
        Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId);
    }
}

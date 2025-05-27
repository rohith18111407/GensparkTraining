using FirstAPI.Models;

namespace FirstAPI.Interfaces
{
    public interface IAppointmentRepository
    {
        IEnumerable<Appointment> GetAll();
        Appointment? GetById(int id);
        void Add(Appointment appointment);
        void Update(Appointment appointment);
        void Delete(int id);
    }
}

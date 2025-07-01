using DoctorPatientAppointment.Models;

namespace DoctorPatientAppointment.Interfaces
{
    public interface IPatientRepository : IRepository<int,Patient>
    {
    }
}

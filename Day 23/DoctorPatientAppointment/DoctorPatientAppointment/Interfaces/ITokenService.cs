using DoctorPatientAppointment.Models;

namespace DoctorPatientAppointment.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(User user);
    }
}
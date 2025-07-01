using DoctorPatientAppointment.Models.DTOs;

namespace DoctorPatientAppointment.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<UserLoginResponse> Login(UserLoginRequest user);
    }
}
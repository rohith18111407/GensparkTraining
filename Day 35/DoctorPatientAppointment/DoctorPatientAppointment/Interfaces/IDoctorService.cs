using DoctorPatientAppointment.Models;
using DoctorPatientAppointment.Models.DTOs;

namespace DoctorPatientAppointment.Interfaces
{
    public interface IDoctorService
    {
        public Task<Doctor> GetDoctByName(string name);
        public Task<ICollection<DoctorsBySpecialityResponseDto>> GetDoctorsBySpeciality(string speciality);
        public Task<Doctor> AddDoctor(DoctorAddRequestDto doctor);
    }
}
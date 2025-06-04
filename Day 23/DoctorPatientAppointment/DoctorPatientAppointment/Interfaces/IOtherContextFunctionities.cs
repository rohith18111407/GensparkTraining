using DoctorPatientAppointment.Models.DTOs;

namespace DoctorPatientAppointment.Interfaces
{
    public interface IOtherContextFunctionities
    {
        public Task<ICollection<DoctorsBySpecialityResponseDto>> GetDoctorsBySpeciality(string specilaity);
    }
}
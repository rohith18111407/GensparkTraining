using DoctorPatientAppointment.Models;
using DoctorPatientAppointment.Models.DTOs;

namespace DoctorPatientAppointment.Misc
{
    public class DoctorMapper
    {
        public Doctor? MapDoctorAddRequestDoctor(DoctorAddRequestDto addRequestDto)
        {
            Doctor doctor = new();
            doctor.Name = addRequestDto.Name;
            doctor.YearsOfExperience = addRequestDto.YearsOfExperience;
            doctor.Email = addRequestDto.Email;
            return doctor;
        }
    }
}
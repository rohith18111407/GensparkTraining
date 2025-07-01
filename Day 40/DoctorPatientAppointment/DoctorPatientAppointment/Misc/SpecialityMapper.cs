using DoctorPatientAppointment.Models;
using DoctorPatientAppointment.Models.DTOs;
using DoctorSpecialities.Models;

namespace DoctorPatientAppointment.Misc
{
    public class SpecialityMapper
    {
        public Speciality? MapSpecialityAddRequestDoctor(SpecialityAddRequestDto addRequestDto)
        {
            Speciality speciality = new();
            speciality.Name = addRequestDto.Name;
            return speciality;
        }

        public DoctorSpeciality MapDoctorSpecility(int doctorId, int specialityId)
        {
            DoctorSpeciality doctorSpeciality = new();
            doctorSpeciality.DoctorId = doctorId;
            doctorSpeciality.SpecialityId = specialityId;
            return doctorSpeciality;
        }
    }
}
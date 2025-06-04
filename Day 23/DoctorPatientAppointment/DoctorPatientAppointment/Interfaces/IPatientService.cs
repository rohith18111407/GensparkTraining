using DoctorPatientAppointment.Models;
using DoctorPatientAppointment.Models.DTOs;

namespace DoctorPatientAppointment.Interfaces
{
    public interface IPatientService
    {
        Task<Patient> AddPatient(PatientAddRequestDto patientDto);
        Task<Patient> GetPatientById(int id);
        Task<IEnumerable<Patient>> GetAllPatients();
    }
}

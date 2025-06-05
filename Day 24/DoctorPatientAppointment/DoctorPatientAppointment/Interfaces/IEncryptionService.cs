using DoctorPatientAppointment.Models;

namespace DoctorPatientAppointment.Interfaces
{
    public interface IEncryptionService
    {
        public Task<EncryptModel> EncryptData(EncryptModel data);
    }
}
using FirstTestAPI.Interfaces;
using FirstTestAPI.Models;

namespace FirstTestAPI.Services
{
    public class PatientService : IPatientService
    {
        private readonly IRepository<int, Patient> _patientRepository;

        public PatientService(IRepository<int, Patient> patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _patientRepository.GetAllAsync();
        }

        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            return await _patientRepository.GetByIdAsync(id);
        }

        public async Task<Patient> AddPatientAsync(Patient patient)
        {
            return await _patientRepository.AddAsync(patient);
        }

        public async Task<Patient?> UpdatePatientAsync(Patient patient)
        {
            return await _patientRepository.UpdateAsync(patient);
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            var existing = await _patientRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            await _patientRepository.DeleteAsync(id);
            return true;
        }
    }
}

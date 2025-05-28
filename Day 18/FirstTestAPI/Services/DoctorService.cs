using FirstTestAPI.Interfaces;
using FirstTestAPI.Models;
using FirstTestAPI.Repositories;

namespace FirstTestAPI.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository repo)
        {
            _doctorRepository = repo;
        }

        public async Task<Doctor> AddDoctorAsync(Doctor doctor)
        {
            var allDoctors = await _doctorRepository.GetAllAsync();
            doctor.Id = allDoctors.Any() ? allDoctors.Max(d => d.Id) + 1 : 1;

            return await _doctorRepository.AddAsync(doctor);
        }

        public async Task<Doctor> DeleteDoctorAsync(int id)
        {
            var existing = await _doctorRepository.GetByIdAsync(id);
            if (existing == null)
                return null!;
            await _doctorRepository.DeleteAsync(id);
            return existing;
        }
        
        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _doctorRepository.GetAllAsync();
        }

        public async Task<Doctor?> GetDoctorByIdAsync(int id)
        {
            return await _doctorRepository.GetByIdAsync(id);
        }

        public async Task<Doctor> UpdateDoctorAsync(Doctor doctor)
        {
            var existing = await _doctorRepository.GetByIdAsync(doctor.Id);
            if (existing == null)
                return null!;

            existing.Name = doctor.Name;
            existing.Status = doctor.Status;
            existing.YearsOfExperience = doctor.YearsOfExperience;

            return await _doctorRepository.UpdateAsync(existing);
        }
    }
}

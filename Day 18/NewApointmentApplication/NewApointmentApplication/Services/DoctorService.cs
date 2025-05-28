using NewApointmentApplication.Interfaces;
using NewApointmentApplication.Models;
using NewApointmentApplication.Models.DTOs;
using NewApointmentApplication.Repositories;

namespace NewApointmentApplication.Services
{
    public class DoctorService
    {
        private readonly DoctorRepository _doctorRepository;
        private readonly SpecialityRepository _specialityRepository;
        private readonly DoctorSpecialityRepository _doctorSpecialityRepository;

        public DoctorService(DoctorRepository doctorRepository,
                             SpecialityRepository specialityRepository,
                             DoctorSpecialityRepository doctorSpecialityRepository)
        {
            _doctorRepository = doctorRepository;
            _specialityRepository = specialityRepository;
            _doctorSpecialityRepository = doctorSpecialityRepository;
        }

        public async Task<Doctor> AddDoctorAsync(DoctorAddRequestDto doctorDto)
        {
            // Create doctor entity
            var doctor = new Doctor
            {
                Name = doctorDto.Name,
                Status = "Active",
                YearsOfExperience = doctorDto.YearsOfExperience
            };

            // Add doctor to database
            var addedDoctor = await _doctorRepository.Add(doctor);

            // Process specialities
            if (doctorDto.Specialities != null && doctorDto.Specialities.Any())
            {
                foreach (var specialityDto in doctorDto.Specialities)
                {
                    // Check if the speciality already exists
                    var existingSpeciality = (await _specialityRepository.GetAll())
                        .FirstOrDefault(s => s.Name.ToLower() == specialityDto.Name.ToLower());

                    Speciality specialityToUse;

                    if (existingSpeciality != null)
                    {
                        specialityToUse = existingSpeciality;
                    }
                    else
                    {
                        // Add new speciality
                        var newSpeciality = new Speciality
                        {
                            Name = specialityDto.Name,
                            Status = "Active"
                        };
                        specialityToUse = await _specialityRepository.Add(newSpeciality);
                    }

                    // Add entry to DoctorSpeciality
                    var doctorSpeciality = new DoctorSpeciality
                    {
                        DoctorId = addedDoctor.Id,
                        SpecialityId = specialityToUse.Id
                    };

                    await _doctorSpecialityRepository.Add(doctorSpeciality);
                }
            }

            return addedDoctor;
        }

        public async Task<Doctor> GetDoctorByIdAsync(int id)
        {
            return await _doctorRepository.Get(id);
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _doctorRepository.GetAll();
        }

        public async Task<Doctor> UpdateDoctorAsync(int id, Doctor updatedDoctor)
        {
            return await _doctorRepository.Update(id, updatedDoctor);
        }

        public async Task<Doctor> DeleteDoctorAsync(int id)
        {
            return await _doctorRepository.Delete(id);
        }
    }
}

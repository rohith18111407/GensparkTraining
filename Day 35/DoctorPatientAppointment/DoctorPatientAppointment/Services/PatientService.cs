using AutoMapper;
using DoctorPatientAppointment.Interfaces;
using DoctorPatientAppointment.Models;
using DoctorPatientAppointment.Models.DTOs;

namespace DoctorPatientAppointment.Services
{
    public class PatientService : IPatientService
    {
        private readonly IRepository<int, Patient> _patientRepository;
        private readonly IRepository<string, User> _userRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IMapper _mapper;

        public PatientService(IRepository<int, Patient> patientRepository,
                              IRepository<string, User> userRepository,
                              IEncryptionService encryptionService,
                              IMapper mapper)
        {
            _patientRepository = patientRepository;
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _mapper = mapper;
        }
        public async Task<Patient> AddPatient(PatientAddRequestDto patientDto)
        {
            try
            {
                var user = _mapper.Map<PatientAddRequestDto, User>(patientDto);
                var encryptedData = await _encryptionService.EncryptData(new EncryptModel
                {
                    Data = patientDto.Password
                });
                user.Password = encryptedData.EncryptedData;
                user.HashKey = encryptedData.HashKey;
                user.Role = "Patient";

                // Save user
                user = await _userRepository.Add(user);

                // Create and save patient
                var patient = new Patient
                {
                    Name = patientDto.Name,
                    Age = patientDto.Age,
                    Email = patientDto.Email,
                    Phone = patientDto.Phone,
                    User = user
                };

                var addedPatient = await _patientRepository.Add(patient);
                return addedPatient;
            }
            catch (Exception e)
            {
                throw new Exception($"Error adding patient: {e.Message}");
            }
        }

        public async Task<Patient> GetPatientById(int id)
        {
            var patient = await _patientRepository.Get(id);
            if (patient == null)
            {
                throw new Exception("Patient not found");
            }
            return patient;
        }

        public async Task<IEnumerable<Patient>> GetAllPatients()
        {
            return await _patientRepository.GetAll();
        }
    }
}

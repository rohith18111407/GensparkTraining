﻿using NewApointmentApplication.Models;
using NewApointmentApplication.Models.DTOs;

namespace NewApointmentApplication.Interfaces
{
    public interface IDoctorService
    {
        public Task<Doctor> GetDoctByName(string name);
        public Task<ICollection<Doctor>> GetDoctorsBySpeciality(string speciality);
        public Task<Doctor> AddDoctor(DoctorAddRequestDto doctor);
    }
}

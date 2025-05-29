﻿using Microsoft.EntityFrameworkCore;
using NewApointmentApplication.Models;
using NewAppointmentApplication.Contexts;

namespace NewApointmentApplication.Repositories
{
    public class DoctorRepository : Repository<int, Doctor>
    {
        public DoctorRepository(ClinicContext clinicContext) : base(clinicContext)
        {
        }

        public override async Task<Doctor> Get(int key)
        {
            var doctor = await _clinicContext.Doctors.SingleOrDefaultAsync(p => p.Id == key);

            return doctor ?? throw new Exception("No doctor with teh given ID");
        }

        public override async Task<IEnumerable<Doctor>> GetAll()
        {
            var doctors = _clinicContext.Doctors;
            if (doctors.Count() == 0)
                throw new Exception("No Doctor in the database");
            return (await doctors.ToListAsync());
        }
    }
}

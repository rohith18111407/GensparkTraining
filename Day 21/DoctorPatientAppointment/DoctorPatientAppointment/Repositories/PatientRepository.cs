using DoctorPatientAppointment.Contexts;
using DoctorPatientAppointment.Interfaces;
using DoctorPatientAppointment.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorPatientAppointment.Repositories
{
    public  class PatinetRepository : Repository<int, Patient> , IPatientRepository
    {
        private readonly ClinicContext _clinicContext;
        public PatinetRepository(ClinicContext clinicContext) : base(clinicContext)
        {   
            _clinicContext = clinicContext;
        }

        public override async Task<Patient> Get(int key)
        {
            var patient = await _clinicContext.Patients.SingleOrDefaultAsync(p => p.Id == key);

            return patient??throw new Exception("No patient with teh given ID");
        }

        public override async Task<IEnumerable<Patient>> GetAll()
        {
            var patients = _clinicContext.Patients;
            if (patients.Count() == 0)
                throw new Exception("No Patients in the database");
            return (await patients.ToListAsync());
        }
    }
}
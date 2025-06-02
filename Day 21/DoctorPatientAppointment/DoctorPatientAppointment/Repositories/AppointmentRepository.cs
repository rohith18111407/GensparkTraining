using DoctorPatientAppointment.Contexts;
using DoctorPatientAppointment.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorPatientAppointment.Repositories
{
    public  class AppointmnetRepository : Repository<string, Appointmnet>
    {
        public AppointmnetRepository(ClinicContext clinicContext) : base(clinicContext)
        {
        }

        public override async Task<Appointmnet> Get(string key)
        {
            var appointment = await _clinicContext.Appointmnets.SingleOrDefaultAsync(p => p.AppointmnetNumber == key);

            return appointment??throw new Exception("No appointmnet with the given ID");
        }

        public override async Task<IEnumerable<Appointmnet>> GetAll()
        {
            var appointments = _clinicContext.Appointmnets;
            if (appointments.Count() == 0)
                throw new Exception("No Appointment in the database");
            return (await appointments.ToListAsync());
        }
    }
}
using DoctorPatientAppointment.Contexts;
using DoctorPatientAppointment.Interfaces;
using DoctorPatientAppointment.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorPatientAppointment.Repositories
{
    public class AppointmnetRepository : Repository<string, Appointmnet>
    {
        public AppointmnetRepository(ClinicContext clinicContext) : base(clinicContext)
        {
        }

        public override async Task<Appointmnet> Get(string key)
        {
            var appointment = await _clinicContext.Appointmnets.SingleOrDefaultAsync(p => p.AppointmnetNumber == key);
            return appointment ?? throw new Exception("No appointment with the given ID");
        }

        public override async Task<IEnumerable<Appointmnet>> GetAll()
        {
            return await _clinicContext.Appointmnets.ToListAsync();
        }

        public async Task<Appointmnet> Add(Appointmnet entity)
        {
            _clinicContext.Appointmnets.Add(entity);
            await _clinicContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Appointmnet> Update(Appointmnet entity)
        {
            var existing = await _clinicContext.Appointmnets.FindAsync(entity.AppointmnetNumber);
            if (existing == null) throw new Exception("Appointment not found");

            existing.AppointmnetDateTime = entity.AppointmnetDateTime;
            existing.Status = entity.Status;
            existing.DoctorId = entity.DoctorId;
            existing.PatientId = entity.PatientId;

            await _clinicContext.SaveChangesAsync();
            return existing;
        }

        public async Task<Appointmnet> Delete(string key)
        {
            var appointment = await _clinicContext.Appointmnets.FindAsync(key);
            if (appointment == null) throw new Exception("Appointment not found");
            _clinicContext.Appointmnets.Remove(appointment);
            await _clinicContext.SaveChangesAsync();
            return appointment;
        }
    }
}
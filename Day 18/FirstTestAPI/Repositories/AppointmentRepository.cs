using FirstTestAPI.Contexts;
using FirstTestAPI.Interfaces;
using FirstTestAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstTestAPI.Repositories
{
    public class AppointmentRepository : Repository<int, Appointment>, IAppointmentRepository
    {
        private readonly ClinicContext _context;

        public AppointmentRepository(ClinicContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId)
        {
            return await _context.Appointments
                                    .Include(a=>a.Doctor)
                                    .Include(a=>a.Patient)
                                    .Where(a=>a.DoctorId==doctorId)
                                    .ToListAsync();
        }
    }
}

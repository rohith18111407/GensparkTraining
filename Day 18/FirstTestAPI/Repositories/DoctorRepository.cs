using FirstTestAPI.Contexts;
using FirstTestAPI.Interfaces;
using FirstTestAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FirstTestAPI.Repositories
{
    public class DoctorRepository : Repository<int,Doctor>,IDoctorRepository
    {
        private readonly ClinicContext _context;
        public DoctorRepository(ClinicContext context) : base(context) 
        {
            _context = context;
        }

        public override async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            var doctors = _context.Doctors;
            if (doctors.Count() == 0)
                throw new Exception("No Doctor in the database");
            return (await doctors.ToListAsync());
        }

        public override async Task<Doctor?> GetByIdAsync(int id)
        {
            var doctor = await _context.Doctors.SingleOrDefaultAsync(p => p.Id == id);

            return doctor ?? throw new Exception("No doctor with teh given ID");
        }

        
    }
}

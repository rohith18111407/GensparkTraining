using FirstTestAPI.Contexts;
using FirstTestAPI.Interfaces;
using FirstTestAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstTestAPI.Repositories
{
    public class PatientRepository : Repository<int,Patient>,IPatientRepository
    {
        private readonly ClinicContext _context;

        public PatientRepository(ClinicContext context) : base(context)
        {
            _context = context;
        }

        
    }
}

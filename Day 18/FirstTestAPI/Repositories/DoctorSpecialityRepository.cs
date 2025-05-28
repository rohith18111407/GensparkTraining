using FirstTestAPI.Contexts;
using FirstTestAPI.Interfaces;
using FirstTestAPI.Models;

namespace FirstTestAPI.Repositories
{
    public class DoctorSpecialityRepository : Repository<int,DoctorSpeciality>,IDoctorSpecialityRepository
    {
        public DoctorSpecialityRepository(ClinicContext context) : base(context)
        {
            
        }
    }
}

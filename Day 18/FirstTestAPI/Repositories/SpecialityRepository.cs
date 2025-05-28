using FirstTestAPI.Contexts;
using FirstTestAPI.Interfaces;
using FirstTestAPI.Models;

namespace FirstTestAPI.Repositories
{
    public class SpecialityRepository : Repository<int,Speciality>,ISpecialityRepository
    {
        public SpecialityRepository(ClinicContext context) : base(context)
        {
            
        }
    }
}

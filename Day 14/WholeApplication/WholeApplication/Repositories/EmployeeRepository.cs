using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WholeApplication.Exceptions;
using WholeApplication.Models;

namespace WholeApplication.Repositories
{
    public class EmployeeRepository : Repository<int,Employee>
    {
        public EmployeeRepository() : base() { }

        public override int GenerateId()
        {
            if(_items.Count == 0)
            {
                return 101;
            }
            else
            {
                return _items.Max(e => e.Id) + 1;
            }
        }

        public override ICollection<Employee> GetAll()
        {
            if(_items.Count == 0)
            {
                throw new CollectionEmptyException("No Employees Found");
            }
            return _items;
        }

        public override Employee GetById(int id)
        {
            var employee = _items.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                throw new KeyNotFoundException("Employee Not Found");
            }
            return employee;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WholeApplication.Interfaces
{
    // K is the type of the primary key used to identify the entity (int in the case of Employee.Id).
    public interface IRepositor<K,T> where T : class
    {
        T Add(T item);
        T Update(T item);
        T Delete(K id);
        T GetById(K id);
        ICollection<T> GetAll();

    }
}

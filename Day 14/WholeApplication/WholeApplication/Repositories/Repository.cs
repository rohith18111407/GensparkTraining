using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WholeApplication.Exceptions;
using WholeApplication.Interfaces;

namespace WholeApplication.Repositories
{
    public abstract class Repository<K,T> : IRepositor<K,T> where T : class
    {
        protected List<T> _items = new List<T>();

        public abstract K GenerateId();
        public abstract ICollection<T> GetAll();
        public abstract T GetById(K id);


        public T Add(T item)
        {
            var id = GenerateId();

            // typeof(T) gets the runtime Type of the generic class (e.g., Employee).
            // .GetProperty("Id") looks for a public property called Id.
            var property = typeof(T).GetProperty("id");

            if(property != null)
            {
                // SetValue(item, id) assigns the generated ID to the entity.
                property.SetValue(item, id);
            }

            // Check if the item already exists in the list
            if (_items.Contains(item))
            {
                throw new DuplicateEntityException("Employee already exists");
            }
            _items.Add(item);
            return item;

        }

        public T Update(T item)
        {
            // item.GetType().GetProperty("Id") → fetches the PropertyInfo for the Id property of the passed object (T).
            // .GetValue(item) → gets the current value of Id from the item.
            // (K) → casts the result to the type of the key (like int).
            // GetById(...) → uses the extracted ID to fetch the existing entity from the list.
            var myItem = GetById((K)item.GetType().GetProperty("Id").GetValue(item));
            if (myItem == null)
            {
                throw new KeyNotFoundException("Item not found");
            }
            var index = _items.IndexOf(myItem);
            _items[index] = item;
            return item;
        }

        public T Delete(K id)
        {
            var item = GetById(id);
            if (item == null)
            {
                throw new KeyNotFoundException("Item not found");
            }
            _items.Remove(item);
            return item;
        }
    }
}

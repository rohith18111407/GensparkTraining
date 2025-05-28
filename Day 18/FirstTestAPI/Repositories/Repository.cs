using FirstTestAPI.Contexts;
using FirstTestAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FirstTestAPI.Repositories
{
    public class Repository<K,T> : IRepository<K,T> where T : class
    {
        protected readonly ClinicContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ClinicContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public virtual async Task<T?> GetByIdAsync(K id) => await _dbSet.FindAsync(id);

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(K id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

    }

}

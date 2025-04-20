using Microsoft.EntityFrameworkCore;
using teamflow.API.Data;
using teamflow.API.Repositories.Interfaces;

namespace teamflow.API.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        public BaseRepository(AppDbContext context) => _context = context;

        public virtual async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<bool> ExistsAsync(Guid id)=>
            await _context.Set<T>().FindAsync(id) != null;

        public virtual async Task<IEnumerable<T>> GetAllAsync()=>
            await _context.Set<T>().ToListAsync();


        public virtual async Task<T?> GetByIdAsync(Guid id) => 
            await _context.Set<T>().FindAsync(id);

        public virtual async Task RemoveAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);

            if(entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}

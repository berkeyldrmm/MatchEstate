using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccessLayer.Concrete
{
    public class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : class
    {
        public MatchEstateDbContext _context { get; set; }
        public DbSet<T> Entity => _context.Set<T>();
        public GenericRepository(MatchEstateDbContext context)
        {
            _context = context;
        }
        public bool Delete(T item)
        {
            EntityEntry<T> entityEntry = Entity.Remove(item);
            return entityEntry.State == EntityState.Deleted;
        }

        public void DeleteRange(IEnumerable<T> items)
        {
            Entity.RemoveRange(items);
        }

        public async Task<bool> Insert(T item)
        {
            EntityEntry<T> entityEntry = await Entity.AddAsync(item);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<T> Read(TKey id)
        {
            return await Entity.FindAsync(id);
        }

        public async Task<IEnumerable<T>> ReadAll()
        {
            return await Entity.ToListAsync();
        }

        public async Task<bool> Update(T item)
        {
            EntityEntry<T> entityEntry = Entity.Update(item);
            return entityEntry.State == EntityState.Modified;
        }
    }
}

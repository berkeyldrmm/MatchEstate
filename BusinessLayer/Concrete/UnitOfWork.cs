using BusinessLayer.Abstract;
using DataAccessLayer.Context;

namespace BusinessLayer.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MatchEstateDbContext _context;

        public UnitOfWork(MatchEstateDbContext context)
        {
            _context = context;
        }

        public Task<int> SaveChanges()
        {
            return _context.SaveChangesAsync();
        }
    }
}

using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Entities;

namespace DataAccessLayer.Concrete
{
    public class LandRepository : GenericRepository<Land, string>, ILandRepository
    {
        public LandRepository(MatchEstateDbContext context) : base(context)
        {
        }
    }
}

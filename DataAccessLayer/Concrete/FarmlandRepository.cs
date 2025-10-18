using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Entities;

namespace DataAccessLayer.Concrete
{
    public class FarmlandRepository : GenericRepository<Farmland, string>, IFarmlandRepository
    {
        public FarmlandRepository(MatchEstateDbContext context) : base(context)
        {
        }
    }
}

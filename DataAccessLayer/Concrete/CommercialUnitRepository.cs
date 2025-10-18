using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Entities;

namespace DataAccessLayer.Concrete
{
    public class CommercialUnitRepository : GenericRepository<CommercialUnit, string>, ICommercialUnitRepository
    {
        public CommercialUnitRepository(MatchEstateDbContext context) : base(context)
        {
        }
    }
}

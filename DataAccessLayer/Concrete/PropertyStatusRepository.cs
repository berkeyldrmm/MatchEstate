using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Entities;

namespace DataAccessLayer.Concrete;

public class PropertyStatusRepository : GenericRepository<PropertyStatus, int>, IPropertyStatusRepository
{
    public PropertyStatusRepository(MatchEstateDbContext context) : base(context)
    {
    }
}

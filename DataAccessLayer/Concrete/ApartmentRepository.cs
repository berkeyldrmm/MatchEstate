using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Entities;

namespace DataAccessLayer.Concrete
{
    public class ApartmentRepository : GenericRepository<Apartment, string>, IApartmentRepository
    {
        public ApartmentRepository(MatchEstateDbContext context) : base(context)
        {
        }
    }
}

using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Entities;

namespace DataAccessLayer.Concrete
{
    public class ShopRepository : GenericRepository<Shop, string>, IShopRepository
    {
        public ShopRepository(MatchEstateDbContext context) : base(context)
        {
        }
    }
}

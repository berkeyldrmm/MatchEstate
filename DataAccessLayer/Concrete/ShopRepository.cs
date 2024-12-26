using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class ShopRepository : GenericRepository<Shop>, IShopDal
    {
        public ShopRepository(MatchEstateDbContext context) : base(context)
        {
        }
    }
}

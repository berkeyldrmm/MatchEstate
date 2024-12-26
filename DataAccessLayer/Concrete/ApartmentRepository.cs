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
    public class ApartmentRepository : GenericRepository<Apartment>, IApartmentDal
    {
        public ApartmentRepository(MatchEstateDbContext context) : base(context)
        {
        }
    }
}

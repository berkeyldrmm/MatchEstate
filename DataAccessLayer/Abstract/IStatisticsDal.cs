using DTOLayer;
using MatchEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IStatisticsDal
    {
        public List<ForSaleOrRentCountDTO> GetForSaleOrRentOfListings(string userId);
        public List<ForSaleOrRentCountDTO> GetForSaleOrRentOfRequests(string userId);
        public List<TypeCountModelDTO> GetPropertyTypesOfListings(string userId);
        public List<TypeCountModelDTO> GetPropertyTypesOfRequests(string userId);
    }
}

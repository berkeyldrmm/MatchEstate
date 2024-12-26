using DTOLayer;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IListingDal : IGenericDal<PropertyListing>
    {
        public Task<IEnumerable<PropertyListing>> GetAllWithClient(string userId);
        public Task<PropertyListing> GetWithClient(string userId, string id);
        public Task<PropertyType> GetPropertyType(int id);
        IEnumerable<PropertyListing> GetRange(IEnumerable<string> Ids);
        public Task<bool> SellListing(string id, string earning);
        public object GetCountsOfListingTypes(string userId);
        public object GetForSaleOrRent(string userId);
        public Task<List<PropertyListing>> GetListingsForRequest(string userId, List<Expression<Func<PropertyListing, bool>>> expressions);
        public IEnumerable<ListingPageDTO> GetByFilters(string userId, List<Expression<Func<PropertyListing, bool>>> expressions, string sort);
        public Task<List<(string listingTitle, decimal earning)>> GetEarningsOfMonth(string userId);
    }
}

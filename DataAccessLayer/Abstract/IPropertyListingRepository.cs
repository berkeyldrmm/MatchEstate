using DTOLayer;
using EntityLayer.Entities;
using System.Linq.Expressions;

namespace DataAccessLayer.Abstract
{
    public interface IPropertyListingRepository : IGenericRepository<PropertyListing, string>
    {
        public Task<IEnumerable<PropertyListing>> GetAllWithClient(string userId);
        public Task<PropertyListing> GetWithClient(string userId, string id);
        public Task<PropertyType> GetPropertyType(int id);
        IEnumerable<PropertyListing> GetRange(IEnumerable<string> Ids);
        public Task<bool> SellListing(string id, string earning);
        public object GetCountsOfListingTypes(string userId);
        public object GetForSaleOrRent(string userId);
        public Task<List<PropertyListing>> GetListingsForRequest(string userId, List<Expression<Func<PropertyListing, bool>>> expressions);
        public (IEnumerable<ListingPageDTO>, int) GetByFilters(string userId, List<Expression<Func<PropertyListing, bool>>> expressions, string sort, int pageNumber, int pageSize);
        public Task<List<(string listingTitle, decimal earning)>> GetEarningsOfMonth(string userId);
        public int GetListingCount(string userId);
    }
}

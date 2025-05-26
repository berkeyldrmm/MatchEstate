using EntityLayer.Entities;
using System.Linq.Expressions;
using Shared.Dtos.PropertyListing;
using Shared.Dtos.PropertyListing.Detail;
using Shared.Dtos.Abstractions;
using Azure.Core;

namespace DataAccessLayer.Abstract
{
    public interface IPropertyListingRepository : IGenericRepository<PropertyListing, string>
    {
        public Task<IEnumerable<PropertyListing>> GetAllWithClient(string userId);
        public IQueryable<PropertyListing> GetWithClient(string userId, string id);
        public Task<PropertyType> GetPropertyType(int id);
        IEnumerable<PropertyListing> GetRange(IEnumerable<string> Ids);
        public Task<bool> FinalizeListing(string userId, string id, string earning, string requestId);
        public object GetCountsOfListingTypes(string userId);
        public object GetForSaleOrRent(string userId);
        public Task<List<PropertyListingCardDto>> GetListingsForRequest(string userId, List<Expression<Func<PropertyListing, bool>>> expressions);
        public (IEnumerable<ListingPageDTO>, int) GetByFilters(string userId, List<Expression<Func<PropertyListing, bool>>> expressions, string sort, int pageNumber, int pageSize);
        public Task<List<(string listingTitle, decimal earning)>> GetEarningsOfMonth(string userId);
        public int GetListingCount(string userId);
        public UpdateListingDto? GetListingForUpdate(string userId, string id);
        public Task<IPropertyListingDetailDto> GetListingDetail(string userId, string id);
    }
}

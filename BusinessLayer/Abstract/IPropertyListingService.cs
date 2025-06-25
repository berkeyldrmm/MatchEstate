using BusinessLayer.Mapping;
using EntityLayer.Entities;
using Shared.Dtos.Abstractions;
using Shared.Dtos.PropertyListing;
using Shared.Dtos.PropertyListing.Detail;

namespace BusinessLayer.Abstract
{
    public interface IPropertyListingService : IGenericService<PropertyListing, string>
    {
        public Task<bool> Insert(string userId, AddListingDTO listingModel);
        public Task<bool> Update(string userId, UpdateListingDto listingModel);
        public Task<IEnumerable<PropertyListing>> GetAllWithClient(string userId);
        public Task<PropertyListing> GetWithClient(string userId, string id);
        public Task<PropertyType> GetPropertyType(int id);
        void DeleteRange(string userId, IEnumerable<string> Ids);
        void DeleteRange(IEnumerable<PropertyListing> listings);
        public Task<bool> FinalizeListing(string userId, string id, string earning, string requestId);
        public object GetCountsOfListingTypes(string userId);
        public object GetForSaleOrRent(string userId);
        public Task<List<PropertyListingCardDto>> GetListingsForRequest(string userId, PropertyRequest request);
        public Task<List<(string listingTitle, decimal earning)>> GetEarningsOfMonth(string userId);
        public (IEnumerable<ListingPageDTO>, int) GetByFilters(string userId, ListingGetByFiltersDTO getByFiltersDTO);
        public UpdateListingDto? GetListingForUpdate(string userId, string id);
        public Task<IPropertyListingDetailDto> GetListingDetail(string userId, string id);
        public Task<IEnumerable<PropertyListing>> GetListingsByClient(string userId, string clientId);
    }
}

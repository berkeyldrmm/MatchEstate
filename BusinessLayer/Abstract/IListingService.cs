using DTOLayer;
using EntityLayer.Entities;

namespace BusinessLayer.Abstract
{
    public interface IListingService : IGenericService<PropertyListing>
    {
        public Task<(bool, string)> Insert(string userId, AddListingDTO listingModel);
        public Task<IEnumerable<PropertyListing>> GetAllWithClient(string userId);
        public Task<PropertyListing> GetWithClient(string userId, string id);
        public Task<PropertyType> GetPropertyType(int id);
        void DeleteRange(IEnumerable<string> Ids);
        public Task<bool> SellListing(string id, string earning);
        public object GetCountsOfListingTypes(string userId);
        public object GetForSaleOrRent(string userId);
        public Task<List<PropertyListing>> GetListingsForRequest(string userId, PropertyRequest request);
        public Task<List<(string listingTitle, decimal earning)>> GetEarningsOfMonth(string userId);
        public (IEnumerable<ListingPageDTO>, int) GetByFilters(string userId, ListingGetByFiltersDTO getByFiltersDTO);
    }
}

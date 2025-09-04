using BusinessLayer.Abstract;
using BusinessLayer.Mapping;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Shared.Dtos.PropertyListing;
using Shared.Dtos.PropertyListing.Detail;
using Shared.Dtos.Abstractions;
using BusinessLayer.Abstract.Factory;

namespace BusinessLayer.Concrete
{
    public class PropertyListingService : IPropertyListingService
    {
        private readonly IPropertyListingRepository _listingRepository;
        private readonly IPropertyServiceFactory _propertyServiceFactory;
        private readonly IUnitOfWork _unitOfWork;

        public PropertyListingService(IPropertyListingRepository listingRepository, IUnitOfWork unitOfWork, IPropertyServiceFactory propertyServiceFactory)
        {
            _listingRepository = listingRepository;
            _unitOfWork = unitOfWork;
            _propertyServiceFactory = propertyServiceFactory;
        }

        public void DeleteRange(string userId, IEnumerable<string> Ids)
        {
            var items = _listingRepository.GetRange(userId, Ids);
            this.DeleteRange(items);
        }

        public void DeleteRange(IEnumerable<PropertyListing> listings)
        {
            _listingRepository.DeleteRange(listings);
        }

        public Task<IEnumerable<PropertyListing>> GetAll()
        {
            return _listingRepository.ReadAll();
        }

        public Task<PropertyListing> GetOne(string id)
        {
            return _listingRepository.Read(id);
        }

        public UpdateListingDto? GetListingForUpdate(string userId, string id)
        {
            return _listingRepository.GetListingForUpdate(userId, id);
        }

        public async Task<bool> Insert(string userId, AddListingDTO dto)
        {
            var property = ListingMapper.MapToProperty(dto);

            bool propertyResult = false;
            switch (property)
            {
                case Shop shop:
                    propertyResult = await _propertyServiceFactory.GetService<Shop>().AddProperty(shop);
                    break;
                case Apartment apartment:
                    propertyResult = await _propertyServiceFactory.GetService<Apartment>().AddProperty(apartment);
                    break;
                case Land land:
                    propertyResult = await _propertyServiceFactory.GetService<Land>().AddProperty(land);
                    break;
                case Farmland farmland:
                    propertyResult = await _propertyServiceFactory.GetService<Farmland>().AddProperty(farmland);
                    break;
                case CommercialUnit commercialUnit:
                    propertyResult = await _propertyServiceFactory.GetService<CommercialUnit>().AddProperty(commercialUnit);
                    break;
            }

            if (!propertyResult)
                return false;

            var listing = ListingMapper.MapToListingEntity(dto, userId, property.ListingId);

            var listingResult = await _listingRepository.Insert(listing);
            if (!listingResult)
            {
                return false;
            }

            int saved = await _unitOfWork.SaveChanges();

            return saved > 0;
        }

        public async Task<IEnumerable<PropertyListing>> GetAllWithClient(string userId)
        {
            return await _listingRepository.GetAllWithClient(userId);
        }
        public async Task<PropertyListing> GetWithClient(string userId, string id)
        {
            return await _listingRepository.GetWithClient(userId, id).FirstOrDefaultAsync();
        }

        public Task<PropertyType> GetPropertyType(int id)
        {
            return _listingRepository.GetPropertyType(id);
        }

        public Task<bool> FinalizeListing(string userId, string id, string earning, string requestId)
        {
            return _listingRepository.FinalizeListing(userId, id, earning, requestId);
        }

        public object GetCountsOfListingTypes(string userId)
        {
            return _listingRepository.GetCountsOfListingTypes(userId);
        }

        public (IEnumerable<ListingPageDTO>, int) GetByFilters(string userId, ListingGetByFiltersDTO getByFiltersDTO)
        {
            var expressions = new List<Expression<Func<PropertyListing, bool>>>();

            if (getByFiltersDTO.PropertyStatusId != "0")
                expressions.Add(i => i.PropertyStatusId == Convert.ToInt32(getByFiltersDTO.PropertyStatusId));

            if (getByFiltersDTO.PropertyType != 0)
                expressions.Add(i => i.PropertyTypeId == getByFiltersDTO.PropertyType);

            if (getByFiltersDTO.Status != "-1")
                expressions.Add(i => i.DealStatus == Convert.ToBoolean(Convert.ToInt16(getByFiltersDTO.Status)));

            if(getByFiltersDTO.MinPrice != null && getByFiltersDTO.MinPrice.Length > 2)
            {
                var minimumFiyat = Convert.ToDecimal(getByFiltersDTO.MinPrice);
                if (minimumFiyat > 0)
                    expressions.Add(i => i.Price >= minimumFiyat);
            }

            if (getByFiltersDTO.MaxPrice != null && getByFiltersDTO.MaxPrice.Length > 2)
            {
                var maximumFiyat = Convert.ToDecimal(getByFiltersDTO.MaxPrice);
                if (maximumFiyat > 0)
                    expressions.Add(i => i.Price <= maximumFiyat);
            }

            if (getByFiltersDTO.Search != null && getByFiltersDTO.Search.Length > 2)
            {
                expressions.Add(i => i.Title.Contains(getByFiltersDTO.Search) || i.City.Contains(getByFiltersDTO.Search) || i.District.Contains(getByFiltersDTO.Search) || i.Neighbourhood.Contains(getByFiltersDTO.Search) || i.Client.NameSurname.Contains(getByFiltersDTO.Search));
            }

            return _listingRepository.GetByFilters(userId, expressions, getByFiltersDTO.Sort, Convert.ToInt16(getByFiltersDTO.PageNumber), Convert.ToInt16(getByFiltersDTO.PageSize));
        }

        public async Task<List<PropertyListingCardDto>> GetListingsForRequest(string userId, PropertyRequest request)
        {
            List<Expression<Func<PropertyListing, bool>>> expressions = new List<Expression<Func<PropertyListing, bool>>>();

            var districts = JsonConvert.DeserializeObject<List<string>>(request.District).ToList();
            if (districts != null || districts.Count != 0)
                expressions.Add(i => districts.Any(d => d == i.District) && request.City == i.City);

            expressions.Add(i => request.MinimumPrice <= i.Price && request.MaximumPrice >= i.Price);

            var numberOfRooms = JsonConvert.DeserializeObject<List<string>>(request.NumberOfRooms).ToList();
            if (numberOfRooms != null || numberOfRooms.Count != 0) {
                if (request.PropertyTypeId == 5)
                {
                    expressions.Add(i => numberOfRooms.Any(o => o == i.Apartment.NumberOfRooms));
                }
                else if (request.PropertyTypeId == 1)
                {
                    expressions.Add(i => numberOfRooms.Any(o => o == i.Shop.NumberOfRooms));
                }
            }

            expressions.Add(i => request.PropertyStatusId == i.PropertyStatusId);

            expressions.Add(i => request.PropertyTypeId == i.PropertyTypeId);

            expressions.Add(i => i.Client.Id != request.Client.Id);

            expressions.Add(i => i.DealStatus == false);

            return await _listingRepository.GetListingsForRequest(userId, expressions);
        }

        public async Task<List<(string listingTitle, decimal earning)>> GetEarningsOfMonth(string userId)
        {
            return await _listingRepository.GetEarningsOfMonth(userId);
        }

        public async Task<bool> Update(string userId, UpdateListingDto dto)
        {
            var listing = await _listingRepository.GetWithClient(userId, dto.ListingId).FirstOrDefaultAsync();
            if (listing == null)
                return false;

            var updatedListing = ListingMapper.MapToPropertyListing(dto, listing, userId);

            var result = await _listingRepository.Update(listing);
            int saved = await _unitOfWork.SaveChanges();

            return result && saved > 0;
        }

        public async Task<IPropertyListingDetailDto> GetListingDetail(string userId, string id)
        {
            return await _listingRepository.GetListingDetail(userId, id);
        }

        public async Task<IEnumerable<PropertyListing>> GetListingsByClient(string userId, string clientId)
        {
            return await _listingRepository.GetListingsByClient(userId, clientId);
        }
    }
}

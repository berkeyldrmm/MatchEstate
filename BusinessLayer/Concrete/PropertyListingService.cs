using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DTOLayer;
using EntityLayer.Entities;
using Newtonsoft.Json;
using System.Globalization;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace BusinessLayer.Concrete
{
    public class PropertyListingService : IPropertyListingService
    {
        private readonly IPropertyListingRepository _listingRepository;
        private readonly IClientService _clientService;
        private readonly IPropertyService _propertyService;
        private readonly IUnitOfWork _unitOfWork;

        public PropertyListingService(IPropertyListingRepository listingRepository, IClientService clientService, IPropertyService propertyService, IUnitOfWork unitOfWork)
        {
            _listingRepository = listingRepository;
            _clientService = clientService;
            _propertyService = propertyService;
            _unitOfWork = unitOfWork;
        }

        public void DeleteRange(IEnumerable<string> Ids)
        {
            var items = _listingRepository.GetRange(Ids);
            _listingRepository.DeleteRange(items);
        }

        public Task<IEnumerable<PropertyListing>> GetAll()
        {
            return _listingRepository.ReadAll();
        }

        public Task<PropertyListing> GetOne(string id)
        {
            return _listingRepository.Read(id);
        }

        public async Task<(bool, string)> Insert(string userId, AddListingDTO listingModel)
        {
            PropertyListing listing = new PropertyListing()
            {
                Id = Guid.NewGuid().ToString() + DateTime.Now.ToString("hhmmss"),
                Title = listingModel.ListingTitle,
                Price = listingModel.Price,
                PropertyStatusId = Convert.ToInt32(listingModel.PropertyStatusId),
                Status = false,
                UserId = userId,
                City = listingModel.City,
                District = listingModel.District,
                Neighbourhood = listingModel.Neighbourhood
            };
            listing.PropertyTypeId = Convert.ToInt32(listingModel.PropertyType);
            listing.PropertyType = await this.GetPropertyType(listing.PropertyTypeId);
            if (listingModel.RadioForClient == "1")
            {
                bool phoneNumberCheck = _clientService.ControlUserPhoneNumber(userId, listingModel.ClientPhoneNumber);
                if (!phoneNumberCheck)
                {
                    return (false, "A client with the same phone number already exists.");
                }

                var client = new Client()
                {
                    Id = Guid.NewGuid().ToString(),
                    NameSurname = listingModel.ClientNameSurname,
                    Email = listingModel.ClientEmail,
                    PhoneNumber = listingModel.ClientPhoneNumber,
                    UserId = userId
                };

                listing.ClientId = client.Id.ToString();
                listing.Client = client;
            }
            else
            {
                listing.ClientId = listingModel.ClientId;
                listing.Client = await _clientService.GetOne(listingModel.ClientId);
            }
            var property = new Property();
            switch (listing.PropertyTypeId)
            {
                case 1:

                    property = new Shop()
                    {
                        Dues = Convert.ToInt32(listingModel.Dues),
                        HasElevator = Convert.ToBoolean(Convert.ToInt32(listingModel.Elevator)),
                        HasParkingLot = Convert.ToBoolean(Convert.ToInt32(listingModel.ParkingLot)),
                        AgeOfBuilding = listingModel.AgeOfBuilding,
                        Floor = Convert.ToInt32(listingModel.Floor),
                        IsFurnished = Convert.ToBoolean(Convert.ToInt32(listingModel.Furnished)),
                        HeatingType = listingModel.HeatingType,
                        NumberOfFloors = Convert.ToInt32(listingModel.NumberOfFloor),
                        IsEligibleForLoan = Convert.ToBoolean(Convert.ToInt32(listingModel.IsEligibleForLoan)),
                        UsageState = listingModel.UsageState,
                        SquareMeterSizeGross = Convert.ToInt32(listingModel.SquareMetersizeGross),
                        ParselNumber = listingModel.ParselNumber,
                        NumberOfRooms = listingModel.NumberOfRooms
                    };

                    break;
                case 2:
                    property = new Land()
                    {
                        BlockNumber = listingModel.BlockNumber,
                        ParselNumber = listingModel.ParselNumber,
                        ZoningStatus = Convert.ToBoolean(Convert.ToInt32(listingModel.ZoningStatus)),
                        LandShareEligibility = Convert.ToBoolean(Convert.ToInt32(listingModel.IsEligibleForLoan)),
                        SheetNumber = listingModel.SheetNumber,
                        TitleSheetState = Convert.ToBoolean(Convert.ToInt32(listingModel.TitleSheetState))
                    };
                    break;
                case 3:
                    property = new CommercialUnit()
                    {
                        BlockNumber = listingModel.BlockNumber,
                        ParselNumber = listingModel.ParselNumber
                    };
                    break;
                case 4:
                    property = new Farmland()
                    {
                        BlockNumber = listingModel.BlockNumber,
                        ParselNumber = listingModel.ParselNumber,
                        SheetNumber = listingModel.SheetNumber,
                        TitleDeedState = Convert.ToBoolean(Convert.ToInt32(listingModel.TitleSheetState)),
                        ZoningStatus = Convert.ToBoolean(Convert.ToInt32(listingModel.ZoningStatus))
                    };
                    break;
                case 5:
                    property = new Apartment()
                    {
                        BlockNumber = listingModel.BlockNumber,
                        Dues = Convert.ToInt32(listingModel.Dues),
                        HasElevator = Convert.ToBoolean(Convert.ToInt32(listingModel.Elevator)),
                        HasParkingLot = Convert.ToBoolean(Convert.ToInt32(listingModel.ParkingLot)),
                        AgeOfBuilding = listingModel.AgeOfBuilding,
                        Floor = Convert.ToInt32(listingModel.Floor),
                        IsFurnished = Convert.ToBoolean(Convert.ToInt32(listingModel.Furnished)),
                        HeatingType = listingModel.HeatingType,
                        NumberOfFloors = Convert.ToInt32(listingModel.NumberOfFloor),
                        IsEligibleForLoan = Convert.ToBoolean(Convert.ToInt32(listingModel.IsEligibleForLoan)),
                        UsageState = listingModel.UsageState,
                        SquareMeterSizeGross = Convert.ToInt32(listingModel.SquareMetersizeGross),
                        ParselNumber = listingModel.ParselNumber,
                        NumberOfRooms = listingModel.NumberOfRooms,
                        IsInResidentalComplex = Convert.ToBoolean(Convert.ToInt32(listingModel.IsInResidentalComplex)),
                        NumberOfBalcony = Convert.ToInt32(listingModel.NumberOfBalcony),
                        NumberOfBathRooms = Convert.ToInt32(listingModel.NumberOfBathrooms)
                    };
                    break;
                default:
                    break;
            }

            property.Id = Guid.NewGuid().ToString();
            property.ListingId = listing.Id;
            property.SquareMeterSize = listingModel.SquareMetersize;
            property.PricePerSquareMeter = Math.Ceiling(listing.Price / Convert.ToInt32(property.SquareMeterSize));
            bool propertyresult = await _propertyService.AddProperty(property);
            if (!propertyresult)
            {
                return (false, "An error occured while saving the listing. Please try again later.");
            }

            listing.AddedDate = DateTime.Now;

            if (listingModel.RadioForCommission == "0")
                listing.Commission = Convert.ToDecimal(listingModel.Commission);
            else
            {
                if (listingModel.PropertyStatusId == "0")
                    listing.Commission = listingModel.Price;
                else
                    listing.Commission = listingModel.Price * 4 / 100;
            }

            listing.Details = listingModel.Details;
            var result = await _listingRepository.Insert(listing);
            int saved = await _unitOfWork.SaveChanges();
            if (result && saved > 0)
            {
                return (true, "Listing has been saved successfully.");
            }

            return (false, "An error occured while saving the listing. Please try again later.");
        }

        public async Task<IEnumerable<PropertyListing>> GetAllWithClient(string userId)
        {
            return await _listingRepository.GetAllWithClient(userId);
        }
        public async Task<PropertyListing> GetWithClient(string userId, string id)
        {
            return await _listingRepository.GetWithClient(userId, id);
        }

        public Task<PropertyType> GetPropertyType(int id)
        {
            return _listingRepository.GetPropertyType(id);
        }

        public Task<bool> SellListing(string id, string earning)
        {
            return _listingRepository.SellListing(id, earning);
        }

        public object GetCountsOfListingTypes(string userId)
        {
            return _listingRepository.GetCountsOfListingTypes(userId);
        }

        public object GetForSaleOrRent(string userId)
        {
            return _listingRepository.GetForSaleOrRent(userId);
        }

        public (IEnumerable<ListingPageDTO>, int) GetByFilters(string userId, ListingGetByFiltersDTO getByFiltersDTO)
        {
            var expressions = new List<Expression<Func<PropertyListing, bool>>>();

            if (getByFiltersDTO.PropertyStatusId != "0")
                expressions.Add(i => i.PropertyStatusId == Convert.ToInt32(getByFiltersDTO.PropertyStatusId));

            if (getByFiltersDTO.PropertyType != 0)
                expressions.Add(i => i.PropertyTypeId == getByFiltersDTO.PropertyType);

            if (getByFiltersDTO.Status != "-1")
                expressions.Add(i => i.Status == Convert.ToBoolean(Convert.ToInt16(getByFiltersDTO.Status)));

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

        public async Task<List<PropertyListing>> GetListingsForRequest(string userId, PropertyRequest request)
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

            expressions.Add(i => i.Status == false);

            return await _listingRepository.GetListingsForRequest(userId, expressions);
        }

        public async Task<List<(string listingTitle, decimal earning)>> GetEarningsOfMonth(string userId)
        {
            return await _listingRepository.GetEarningsOfMonth(userId);
        }

        public async Task<(bool, string)> Update(string userId, string listingId, AddListingDTO listingModel)
        {
            var listing = await _listingRepository.GetWithClient(userId, listingId);
            if (listing == null)
            {
                return (false, "Listing not found.");
            }

            listing.Title = listingModel.ListingTitle;
            listing.Price = listingModel.Price;
            //listing.PropertyStatusId = listingModel.PropertyStatusId == "1" ? "For Sale" : "For Rent";
            listing.City = listingModel.City;
            listing.District = listingModel.District;
            listing.Neighbourhood = listingModel.Neighbourhood;
            listing.Details = listingModel.Details;

            listing.PropertyTypeId = Convert.ToInt32(listingModel.PropertyType);
            listing.PropertyType = await this.GetPropertyType(listing.PropertyTypeId);

            if (listingModel.RadioForClient == "1")
            {
                bool phoneNumberCheck = _clientService.ControlUserPhoneNumber(userId, listingModel.ClientPhoneNumber);
                if (!phoneNumberCheck)
                {
                    return (false, "A client with the same phone number already exists.");
                }

                var client = new Client()
                {
                    Id = listing.ClientId ?? Guid.NewGuid().ToString(),
                    NameSurname = listingModel.ClientNameSurname,
                    Email = listingModel.ClientEmail,
                    PhoneNumber = listingModel.ClientPhoneNumber,
                    UserId = userId
                };

                listing.ClientId = client.Id;
                listing.Client = client;
            }
            else
            {
                listing.ClientId = listingModel.ClientId;
                listing.Client = await _clientService.GetOne(listingModel.ClientId);
            }

            Property property = new Property();

            switch (listing.PropertyTypeId)
            {
                case 1:
                    var shop = listing.Shop;
                    if (shop != null)
                    {
                        shop.Id = listing.Shop.Id;
                        shop.Dues = Convert.ToInt32(listingModel.Dues);
                        shop.HasElevator = Convert.ToBoolean(Convert.ToInt32(listingModel.Elevator));
                        shop.HasParkingLot = Convert.ToBoolean(Convert.ToInt32(listingModel.ParkingLot));
                        shop.AgeOfBuilding = listingModel.AgeOfBuilding;
                        shop.Floor = Convert.ToInt32(listingModel.Floor);
                        shop.IsFurnished = Convert.ToBoolean(Convert.ToInt32(listingModel.Furnished));
                        shop.HeatingType = listingModel.HeatingType;
                        shop.NumberOfFloors = Convert.ToInt32(listingModel.NumberOfFloor);
                        shop.IsEligibleForLoan = Convert.ToBoolean(Convert.ToInt32(listingModel.IsEligibleForLoan));
                        shop.UsageState = listingModel.UsageState;
                        shop.SquareMeterSizeGross = Convert.ToInt32(listingModel.SquareMetersizeGross);
                        shop.ParselNumber = listingModel.ParselNumber;
                        shop.NumberOfRooms = listingModel.NumberOfRooms;
                        property = shop;
                    }
                    break;
                case 2:
                    var land = listing.Land;
                    if (land != null)
                    {
                        land.Id = listing.Land.Id;
                        land.BlockNumber = listingModel.BlockNumber;
                        land.ParselNumber = listingModel.ParselNumber;
                        land.ZoningStatus = Convert.ToBoolean(Convert.ToInt32(listingModel.ZoningStatus));
                        land.LandShareEligibility = Convert.ToBoolean(Convert.ToInt32(listingModel.IsEligibleForLoan));
                        land.SheetNumber = listingModel.SheetNumber;
                        land.TitleSheetState = Convert.ToBoolean(Convert.ToInt32(listingModel.TitleSheetState));
                        property = land;
                    }
                    break;
                case 3:
                    var commercialUnit = listing.CommercialUnit;
                    if (commercialUnit != null)
                    {
                        commercialUnit.Id = listing.CommercialUnit.Id;
                        commercialUnit.BlockNumber = listingModel.BlockNumber;
                        commercialUnit.ParselNumber = listingModel.ParselNumber;
                        property = commercialUnit;
                    }
                    break;
                case 4:
                    var farmland = listing.Farmland;
                    if (farmland != null)
                    {
                        farmland.Id = listing.Farmland.Id;
                        farmland.BlockNumber = listingModel.BlockNumber;
                        farmland.ParselNumber = listingModel.ParselNumber;
                        farmland.SheetNumber = listingModel.SheetNumber;
                        farmland.TitleDeedState = Convert.ToBoolean(Convert.ToInt32(listingModel.TitleSheetState));
                        farmland.ZoningStatus = Convert.ToBoolean(Convert.ToInt32(listingModel.ZoningStatus));
                        property = farmland;
                    }
                    break;
                case 5:
                    var apartment = listing.Apartment;
                    if (apartment != null)
                    {
                        apartment.Id = listing.Apartment.Id;
                        apartment.BlockNumber = listingModel.BlockNumber;
                        apartment.Dues = Convert.ToInt32(listingModel.Dues);
                        apartment.HasElevator = Convert.ToBoolean(Convert.ToInt32(listingModel.Elevator));
                        apartment.HasParkingLot = Convert.ToBoolean(Convert.ToInt32(listingModel.ParkingLot));
                        apartment.AgeOfBuilding = listingModel.AgeOfBuilding;
                        apartment.Floor = Convert.ToInt32(listingModel.Floor);
                        apartment.IsFurnished = Convert.ToBoolean(Convert.ToInt32(listingModel.Furnished));
                        apartment.HeatingType = listingModel.HeatingType;
                        apartment.NumberOfFloors = Convert.ToInt32(listingModel.NumberOfFloor);
                        apartment.IsEligibleForLoan = Convert.ToBoolean(Convert.ToInt32(listingModel.IsEligibleForLoan));
                        apartment.UsageState = listingModel.UsageState;
                        apartment.SquareMeterSizeGross = Convert.ToInt32(listingModel.SquareMetersizeGross);
                        apartment.ParselNumber = listingModel.ParselNumber;
                        apartment.NumberOfRooms = listingModel.NumberOfRooms;
                        apartment.IsInResidentalComplex = Convert.ToBoolean(Convert.ToInt32(listingModel.IsInResidentalComplex));
                        apartment.NumberOfBalcony = Convert.ToInt32(listingModel.NumberOfBalcony);
                        apartment.NumberOfBathRooms = Convert.ToInt32(listingModel.NumberOfBathrooms);
                        property = apartment;
                    }
                    break;
                default:
                    break;
            }

            bool propertyResult = await _propertyService.UpdateProperty(property);
            if (!propertyResult)
            {
                return (false, "An error occurred while updating the property.");
            }

            if (listingModel.RadioForCommission == "0")
                listing.Commission = Convert.ToDecimal(listingModel.Commission);
            else
                listing.Commission = listingModel.PropertyStatusId == "0" ? listingModel.Price : listingModel.Price * 4 / 100;

            var result = await _listingRepository.Update(listing);
            int saved = await _unitOfWork.SaveChanges();

            if (result && saved > 0)
            {
                return (true, "Listing has been updated successfully.");
            }

            return (false, "An error occurred while updating the listing. Please try again later.");
        }

    }
}

using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DTOLayer;
using EntityLayer.Entities;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace BusinessLayer.Concrete
{
    public class ListingService : IListingService
    {
        private readonly IListingDal _listingRepository;
        private readonly IClientService _clientService;
        private readonly IPropertyService _propertyService;
        private readonly IUnitOfWork _unitOfWork;

        public ListingService(IListingDal listingRepository, IClientService clientService, IPropertyService propertyService, IUnitOfWork unitOfWork)
        {
            _listingRepository = listingRepository;
            _clientService = clientService;
            _propertyService = propertyService;
            _unitOfWork = unitOfWork;
        }

        public bool DeleteAsync(PropertyListing item)
        {
            return _listingRepository.Delete(item);
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
                IsForSaleOrRent = listingModel.IsForSaleOrRent,
                IsSoldOrRented = false,
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
                if (listingModel.IsForSaleOrRent == "0")
                    listing.Commission = listingModel.Price;
                else
                    listing.Commission = listingModel.Price * 4 / 100;
            }

            if (listingModel.IsForSaleOrRent == "0")
                listing.IsForSaleOrRent = "For Rent";
            else
                listing.IsForSaleOrRent = "For Sale";

            listing.Details = listingModel.Details;
            var result = await _listingRepository.Insert(listing);
            int saved = await _unitOfWork.SaveChanges();
            if (result && saved > 0)
            {
                return (true, "Listing has been saved successfully.");
            }

            return (false, "An error occured while saving the listing. Please try again later.");
        }

        public Task<bool> Update(PropertyListing item)
        {
            return _listingRepository.Update(item);
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

        public IEnumerable<ListingPageDTO> GetByFilters(string userId, ListingGetByFiltersDTO getByFiltersDTO)
        {
            var expressions = new List<Expression<Func<PropertyListing, bool>>>();

            if (getByFiltersDTO.IsForSaleOrRent != "0")
                expressions.Add(i => i.IsForSaleOrRent == getByFiltersDTO.IsForSaleOrRent);

            if (getByFiltersDTO.PropertyType != 0)
                expressions.Add(i => i.PropertyTypeId == getByFiltersDTO.PropertyType);

            if (getByFiltersDTO.Status != "-1")
                expressions.Add(i => i.IsSoldOrRented == Convert.ToBoolean(Convert.ToInt16(getByFiltersDTO.Status)));

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

            return _listingRepository.GetByFilters(userId, expressions, getByFiltersDTO.Sort);
        }

        public async Task<List<PropertyListing>> GetListingsForRequest(string userId, PropertyRequest request)
        {
            List<Expression<Func<PropertyListing, bool>>> expressions = new List<Expression<Func<PropertyListing, bool>>>();

            expressions.Add(i => JsonConvert.DeserializeObject<List<string>>(request.District).ToList().Any(s => s == i.District) && request.City==i.City);

            expressions.Add(i => request.MinimumPrice <= i.Price && request.MaximumPrice >= i.Price);

            if (request.PropertyTypeId == 5)
            {
                expressions.Add(i => JsonConvert.DeserializeObject<List<string>>(request.NumberOfRooms).ToList().Any(o => o == i.Apartment.NumberOfRooms));
            }
            else if (request.PropertyTypeId == 1)
            {
                expressions.Add(i => JsonConvert.DeserializeObject<List<string>>(request.NumberOfRooms).ToList().Any(o => o == i.Shop.NumberOfRooms));
            }

            expressions.Add(i => request.IsForSaleOrRent == i.IsForSaleOrRent);

            expressions.Add(i => request.PropertyTypeId == i.PropertyTypeId);

            expressions.Add(i => i.Client.Id != request.Client.Id);

            expressions.Add(i => i.IsSoldOrRented == false);

            return await _listingRepository.GetListingsForRequest(userId, expressions);
        }

        public async Task<List<(string listingTitle, decimal earning)>> GetEarningsOfMonth(string userId)
        {
            return await _listingRepository.GetEarningsOfMonth(userId);
        }

        public async Task<bool> Insert(PropertyListing item)
        {
            return await _listingRepository.Insert(item);
        }
    }
}

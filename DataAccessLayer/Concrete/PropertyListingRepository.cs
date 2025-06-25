using BusinessLayer.Mapping;
using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Shared.Services;
using Shared.Dtos.PropertyListing;
using Shared.Dtos.PropertyListing.Detail;
using Shared.Dtos.Abstractions;

namespace DataAccessLayer.Concrete;

public class PropertyListingRepository : GenericRepository<PropertyListing, string>, IPropertyListingRepository
{
    public DbSet<PropertyType> PropertyType => _context.Set<PropertyType>();
    public PropertyListingRepository(MatchEstateDbContext context) : base(context)
    {
    }

    public IQueryable<PropertyListing> EntityOfUser(string userId) => Entity.Include(l => l.PropertyStatus).Where(i => i.UserId == userId);

    public async Task<IEnumerable<PropertyListing>> GetAllWithClient(string userId)
    {
        return await EntityOfUser(userId).Include(i => i.Client).Include(i => i.PropertyType).ToListAsync();
    }

    public IQueryable<PropertyListing> GetWithClient(string userId, string id)
    {
        return EntityOfUser(userId)
            .Include(i => i.Client)
            .Include(i => i.PropertyType)
            .Include(i => i.Land)
            .Include(i => i.Apartment)
            .Include(i => i.CommercialUnit)
            .Include(i => i.Shop)
            .Include(i => i.Farmland)
            .Where(i => i.Id == id);
    }

    public UpdateListingDto? GetListingForUpdate(string userId, string id)
    {
        return GetWithClient(userId, id)
            .Select(ListingMapper.MapToUpdateListingDto)
            .FirstOrDefault();
    }

    public async Task<PropertyType?> GetPropertyType(int id)
    {
        return await PropertyType.FirstOrDefaultAsync(i => i.Id == id);
    }

    public IEnumerable<PropertyListing> GetRange(string userId, IEnumerable<string> Ids)
    {
        return EntityOfUser(userId).Where(listing => Ids.Contains(listing.Id)).ToList();
    }

    public async Task<bool> FinalizeListing(string userId, string id, string earning, string requestId)
    {
        PropertyListing listing = await EntityOfUser(userId).FirstOrDefaultAsync(l => l.Id == id);

        if(listing is not null)
        {
            listing.DealStatus = true;
            listing.Earning = Convert.ToDecimal(earning);
            listing.DealDate = DateTime.Now;
            if (requestId != null && requestId != "0")
                listing.PropertyRequestId = requestId;

            bool result = await Update(listing);
            return result;
        }

        return false;
    }

    public object GetCountsOfListingTypes(string userId)
    {
        return new
        {
            CountOfLand = EntityOfUser(userId).Where(i => i.PropertyTypeId == 2).Count(),
            CountOfFarmland = EntityOfUser(userId).Where(i => i.PropertyTypeId == 4).Count(),
            CountOfShop = EntityOfUser(userId).Where(i => i.PropertyTypeId == 1).Count(),
            CountOfCommercialUnit = EntityOfUser(userId).Where(i => i.PropertyTypeId == 3).Count(),
            CountOfApartment = EntityOfUser(userId).Where(i => i.PropertyTypeId == 5).Count()
        };
        
    }

    public object GetForSaleOrRent(string userId)
    {
        return new
        {
            //ForSale = EntityOfUser(userId).Where(i => i.PropertyStatusId == "For Sale").Count(),
            //ForRent = EntityOfUser(userId).Where(i => i.PropertyStatusId == "For Rent").Count()
        };
    }

    public (IEnumerable<ListingPageDTO>, int) GetByFilters(string userId, List<Expression<Func<PropertyListing, bool>>> expressions, string sort, int pageNumber, int pageSize)
    {
        IQueryable<PropertyListing> query = EntityOfUser(userId).Include(i => i.Client).Include(i => i.PropertyType);

        foreach (var expression in expressions)
        {
            query = query.Where(expression);
        }

        if (sort != null)
        {
            if (sort.ToLower() == "newest")
            {
                query = query.OrderByDescending(i => i.AddedDate);
            }
            if (sort.ToLower() == "oldest")
            {
                query = query.OrderBy(i => i.AddedDate);
            }
            if (sort.ToLower() == "ascending")
            {
                query = query.OrderBy(i => i.Price);
            }
            if (sort.ToLower() == "descending")
            {
                query = query.OrderByDescending(i => i.Price);
            }
        }

        int totalCountOfListings = query.Count();

        query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        IEnumerable<ListingPageDTO> listings = query.Select(l => new ListingPageDTO
        {
            Id = l.Id,
            ClientNameSurname = l.Client.NameSurname,
            PropertyTypeId = l.PropertyTypeId,
            PropertyType = l.PropertyType.PropertyName,
            ListingTitle = l.Title,
            Price = l.Price.ToString(),
            City = l.City,
            District = l.District,
            PropertyStatusName = l.PropertyStatus.Name,
            Commission = l.Commission.ToString(),
            Earning = l.Earning.ToString(),
            Status = l.DealStatus,
            AddedDate = l.AddedDate.Write()
        });

        return (listings, totalCountOfListings);
    }

    public async Task<List<PropertyListingCardDto>> GetListingsForRequest(string userId, List<Expression<Func<PropertyListing, bool>>> expressions)
    {
        IQueryable<PropertyListing> query = EntityOfUser(userId)
            .Include(i => i.Client)
            .Include(i => i.Shop)
            .Include(i => i.Apartment)
            .Include(i => i.PropertyType);
        
        foreach (var expression in expressions)
        {
            query = query.Where(expression);
        }
        
        var listings = await query.Select(l => new PropertyListingCardDto
        {
            Id = l.Id,
            Title = l.Title,
            AddedDate = l.AddedDate.Write(),
            Address = $"{l.District} / {l.City}",
            ClientNameSurname = l.Client.NameSurname,
            Price = l.Price.ToString(),
            PropertyStatus = l.PropertyStatus.Name,
            PropertyType = l.PropertyType.PropertyName
        }).ToListAsync();

        return listings;
    }

    public async Task<List<(string listingTitle, decimal earning)>> GetEarningsOfMonth(string userId)
    {
        var earningsOfMonth = await EntityOfUser(userId)
            .Where(i => i.DealStatus)
            .Where(i => i.DealDate != null &&
                i.DealDate.Value.Month == DateTime.Now.Month &&
                i.DealDate.Value.Year == DateTime.Now.Year)
            .Select(i => new { i.Title, i.Earning })
            .ToListAsync();
        
        List<(string ilanBaslik, decimal kazanc)> earningsOfMonthTuple = new List<(string listingTitle, decimal earning)>();
        foreach (var earning in earningsOfMonth)
        {
            earningsOfMonthTuple.Add((earning.Title, earning.Earning));
        }
        return earningsOfMonthTuple;
    }

    public int GetListingCount(string userId)
    {
        return EntityOfUser(userId).Count();
    }

    public async Task<IPropertyListingDetailDto> GetListingDetail(string userId, string id)
    {
        var query = EntityOfUser(userId).Where(i => i.Id == id);
        PropertyListingDetailDto dto = new();

        var propertyId = await query.Select(l=>l.PropertyTypeId).FirstOrDefaultAsync();
        switch (propertyId)
        {
            case 1:
                dto = await query.Select(l => new ShopListingDetailDto
                {
                    Id = l.Id,
                    AddedDate = l.AddedDate.Write(),
                    Address = $"{l.Neighbourhood}, {l.District}, {l.City}",
                    Title = l.Title,
                    ClientNameSurname = l.Client.NameSurname,
                    ClientEmailAddress = l.Client.Email,
                    ClientPhoneNumber = l.Client.PhoneNumber,
                    Price = l.Price.ToString(),
                    PropertyStatus = l.PropertyStatus.Name,
                    PropertyTypeId = l.PropertyTypeId,
                    PropertyType = l.PropertyType.PropertyName,
                    Details = l.Details,
                    Commission = l.Commission.ToString(),
                    DealStatus = l.DealStatus,
                    Earning = l.Earning.ToString(),
                    AgeOfBuilding = l.Shop.AgeOfBuilding,
                    BlockNumber = l.Shop.BlockNumber,
                    Dues = l.Shop.Dues.ToString(),
                    Elevator = l.Shop.HasElevator,
                    EligibityForLoan = l.Shop.IsEligibleForLoan,
                    Floor = l.Shop.Floor.ToString(),
                    Furnished = l.Shop.IsFurnished,
                    GrossSquareMetersize = l.Shop.SquareMeterSizeGross.ToString(),
                    HeatingType = l.Shop.HeatingType,
                    NumberOfFloor = l.Shop.NumberOfFloors.ToString(),
                    NumberOfRooms = l.Shop.NumberOfRooms,
                    ParkingLot = l.Shop.HasParkingLot,
                    ParselNumber = l.Shop.ParselNumber,
                    PricePerSquareMetersize = l.Shop.PricePerSquareMeter.ToString(),
                    SquareMetersize = l.Shop.SquareMeterSize,
                    UsageState = l.Shop.UsageState
                }).FirstOrDefaultAsync();
                break;
            case 2:
                dto = await query.Select(l => new LandListingDetailDto
                {
                    Id = l.Id,
                    AddedDate = l.AddedDate.Write(),
                    Address = $"{l.Neighbourhood}, {l.District}, {l.City}",
                    Title = l.Title,
                    ClientNameSurname = l.Client.NameSurname,
                    ClientEmailAddress = l.Client.Email,
                    ClientPhoneNumber = l.Client.PhoneNumber,
                    Price = l.Price.ToString(),
                    PropertyStatus = l.PropertyStatus.Name,
                    PropertyTypeId = l.PropertyTypeId,
                    PropertyType = l.PropertyType.PropertyName,
                    Details = l.Details,
                    Commission = l.Commission.ToString(),
                    Earning = l.Earning.ToString(),
                    DealStatus = l.DealStatus,
                    BlockNumber = l.Land.BlockNumber,
                    LandShareEligibity = l.Land.LandShareEligibility,
                    ParselNumber = l.Land.ParselNumber,
                    PricePerSquareMetersize = l.Land.PricePerSquareMeter.ToString(),
                    SheetNumber = l.Land.SheetNumber,
                    SquareMetersize = l.Land.SquareMeterSize,
                    ZoningStatus = l.Land.ZoningStatus
                }).FirstOrDefaultAsync();
                break;
            case 3:
                dto = await query.Select(l => new CommercialUnitListingDetailDto
                {
                    Id = l.Id,
                    AddedDate = l.AddedDate.Write(),
                    Address = $"{l.Neighbourhood}, {l.District}, {l.City}",
                    Title = l.Title,
                    ClientNameSurname = l.Client.NameSurname,
                    ClientEmailAddress = l.Client.Email,
                    ClientPhoneNumber = l.Client.PhoneNumber,
                    Price = l.Price.ToString(),
                    PropertyStatus = l.PropertyStatus.Name,
                    PropertyTypeId = l.PropertyTypeId,
                    PropertyType = l.PropertyType.PropertyName,
                    Details = l.Details,
                    Commission = l.Commission.ToString(),
                    Earning = l.Earning.ToString(),
                    DealStatus = l.DealStatus,
                    BlockNumber = l.CommercialUnit.BlockNumber,
                    ParselNumber = l.CommercialUnit.ParselNumber,
                    SquareMetersize = l.CommercialUnit.SquareMeterSize,
                    PricePerSquareMetersize = l.CommercialUnit.PricePerSquareMeter.ToString()
                }).FirstOrDefaultAsync();
                break;
            case 4:
                dto = await query.Select(l => new FarmlandListingDetailDto
                {
                    Id = l.Id,
                    AddedDate = l.AddedDate.Write(),
                    Address = $"{l.Neighbourhood}, {l.District}, {l.City}",
                    Title = l.Title,
                    ClientNameSurname = l.Client.NameSurname,
                    ClientEmailAddress = l.Client.Email,
                    ClientPhoneNumber = l.Client.PhoneNumber,
                    Price = l.Price.ToString(),
                    PropertyStatus = l.PropertyStatus.Name,
                    PropertyTypeId = l.PropertyTypeId,
                    PropertyType = l.PropertyType.PropertyName,
                    Details = l.Details,
                    Commission = l.Commission.ToString(),
                    Earning = l.Earning.ToString(),
                    DealStatus = l.DealStatus,
                    BlockNumber = l.Farmland.BlockNumber,
                    ParselNumber = l.Farmland.ParselNumber,
                    PricePerSquareMetersize = l.Farmland.PricePerSquareMeter.ToString(),
                    SheetNumber = l.Farmland.SheetNumber,
                    SquareMetersize = l.Farmland.SquareMeterSize,
                    TitleDeedState = l.Farmland.TitleDeedState,
                    ZoningStatus = l.Farmland.ZoningStatus
                }).FirstOrDefaultAsync();
                break;
            case 5:
                dto = await query.Select(l => new ApartmentListingDetailDto
                {
                    Id = l.Id,
                    AddedDate = l.AddedDate.Write(),
                    Address = $"{l.Neighbourhood}, {l.District}, {l.City}",
                    Title = l.Title,
                    ClientNameSurname = l.Client.NameSurname,
                    ClientEmailAddress = l.Client.Email,
                    ClientPhoneNumber = l.Client.PhoneNumber,
                    Price = l.Price.ToString(),
                    PropertyStatus = l.PropertyStatus.Name,
                    PropertyTypeId = l.PropertyTypeId,
                    PropertyType = l.PropertyType.PropertyName,
                    Commission = l.Commission.ToString(),
                    Earning = l.Earning.ToString(),
                    DealStatus = l.DealStatus,
                    Details = l.Details,
                    AgeOfBuilding = l.Apartment.AgeOfBuilding,
                    BlockNumber = l.Apartment.BlockNumber,
                    Dues = l.Apartment.Dues.ToString(),
                    Elevator = l.Apartment.HasElevator,
                    EligibityForLoan = l.Apartment.IsEligibleForLoan,
                    Floor = l.Apartment.Floor.ToString(),
                    Furnished = l.Apartment.IsFurnished,
                    GrossSquareMetersize = l.Apartment.SquareMeterSizeGross.ToString(),
                    HeatingType = l.Apartment.HeatingType,
                    NumberOfFloor = l.Apartment.NumberOfFloors.ToString(),
                    NumberOfRooms = l.Apartment.NumberOfRooms,
                    ParkingLot = l.Apartment.HasParkingLot,
                    ParselNumber = l.Apartment.ParselNumber,
                    PricePerSquareMetersize = l.Apartment.PricePerSquareMeter.ToString(),
                    SquareMetersize = l.Apartment.SquareMeterSize,
                    IsResidentalComplex = l.Apartment.IsInResidentalComplex,
                    NumberOfBalcony = l.Apartment.NumberOfBalcony.ToString(),
                    NumberOfBathrooms = l.Apartment.NumberOfBathRooms.ToString(),
                    UsageState = l.Apartment.UsageState
                }).FirstOrDefaultAsync();
                break;
            default:
                break;
        }

        return dto;
    }

    public async Task<IEnumerable<PropertyListing>> GetListingsByClient(string userId, string clientId)
    {
        return await EntityOfUser(userId).Where(l => l.ClientId == clientId).ToListAsync();
    }
}

using BusinessLayer.Mapping;
using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Shared.Services;
using Shared.Dtos.PropertyListing;

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

    public IEnumerable<PropertyListing> GetRange(IEnumerable<string> Ids)
    {
        return Entity.Where(listing => Ids.Contains(listing.Id)).ToList();
    }

    public async Task<bool> SellListing(string id, string earning)
    {
        PropertyListing listing = await Read(id);
        listing.DealStatus = true;
        listing.Earning = Convert.ToDecimal(earning);
        listing.DealDate = DateTime.Now;
        bool result = await Update(listing);
        return result;
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

    public async Task<List<PropertyListing>> GetListingsForRequest(string userId, List<Expression<Func<PropertyListing, bool>>> expressions)
    {
        IQueryable<PropertyListing> query = EntityOfUser(userId).Include(i => i.Client).Include(i => i.Shop).Include(i => i.Apartment).Include(i => i.PropertyType);
        
        foreach (var expression in expressions)
        {
            query = query.Where(expression);
        }

        return await query.ToListAsync();
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
}

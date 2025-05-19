using BusinessLayer.Mapping;
using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Shared.Services;
using Shared.Dtos.PropertyRequest;

namespace DataAccessLayer.Concrete
{
    public class PropertyRequestRepository : GenericRepository<PropertyRequest, string>, IPropertyRequestRepository
    {
        public DbSet<PropertyType> PropertyType => _context.Set<PropertyType>();
        public PropertyRequestRepository(MatchEstateDbContext context) : base(context)
        {
        }
        public IQueryable<PropertyRequest> EntityOfUser(string userId) => Entity.Include(l => l.PropertyStatus).Where(i => i.UserId == userId);
        public async Task<IEnumerable<PropertyRequest>> GetAllWithClient(string userId)
        {
            return await EntityOfUser(userId).Include(t => t.Client).Include(t => t.PropertyType).ToListAsync();
        }

        public async Task<PropertyType> GetPropertyType(int id)
        {
            return await PropertyType.FirstOrDefaultAsync(t => t.Id == id);
        }

        public IEnumerable<PropertyRequest> GetRange(string userId, IEnumerable<string> Ids)
        {
            return EntityOfUser(userId).Where(request => Ids.Contains(request.Id)).ToList();
        }

        public IQueryable<PropertyRequest> GetWithClient(string userId, string id)
        {
            return EntityOfUser(userId)
                .Include(r=>r.PropertyStatus)
                .Include(t => t.Client)
                .Include(t => t.PropertyType)
                .Where(t => t.Id == id);
        }

        public UpdateRequestDto? GetRequestForUpdate(string userId, string id)
        {
            return GetWithClient(userId, id)
                .Select(RequestMapper.MapToUpdateRequestDto)
                .FirstOrDefault();
        }

        public object GetCountsOfRequestTypes(string userId)
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

        public (IEnumerable<RequestPageDTO>, int) GetByFilters(string userId, List<Expression<Func<PropertyRequest, bool>>> expressions, string sort, int pageNumber, int pageSize)
        {
            IQueryable<PropertyRequest> query = EntityOfUser(userId).Include(i => i.Client).Include(i => i.PropertyType);

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
            }

            int totalRequestCount = query.Count();

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            IEnumerable<RequestPageDTO> DTOQuery = query.Select(r => new RequestPageDTO
            {
                Id = r.Id,
                Title = r.Title,
                Type = r.PropertyType.PropertyName,
                NameSurname = r.Client.NameSurname,
                City = r.City,
                PropertyStatus = r.PropertyStatus.Name,
                MinPrice = r.MinimumPrice.ToString(),
                MaxPrice = r.MaximumPrice.ToString(),
                AddedDate = r.AddedDate.Write()
            });

            return (DTOQuery, totalRequestCount);
        }

        public async Task<List<PropertyRequest>> GetRequestsForListing(string userId, List<Expression<Func<PropertyRequest, bool>>> expressions)
        {
            IQueryable<PropertyRequest> query = EntityOfUser(userId).Include(t => t.Client).Include(t => t.PropertyType);
            foreach (var expression in expressions)
            {
                query = query.Where(expression);
            }

            return await query.ToListAsync();
        }

        public int GetRequestCount(string userId)
        {
            return EntityOfUser(userId).Count();
        }
    }
}

using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos.Statistics;

namespace DataAccessLayer.Concrete
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly MatchEstateDbContext _context;

        private readonly List<int> propertyTypeIds = new List<int> { 1, 2, 3, 4, 5 };
        public StatisticsRepository(MatchEstateDbContext context)
        {
            _context = context;
        }

        public List<TypeCountModelDTO> GetPropertyTypesOfListings(string userId)
        {
            return _context.PropertyListings
                .Where(l => propertyTypeIds.Contains(l.PropertyTypeId))
                .Where(l=>l.UserId == userId)
                .GroupBy(l => l.PropertyTypeId)
                .Select(l => new TypeCountModelDTO
                {
                    PropertyTypeId = l.Key,
                    Count = l.Count()
                })
                .ToList();
        }

        public List<TypeCountModelDTO> GetPropertyTypesOfRequests(string userId)
        {
            return _context.PropertyRequests
                .Where(r => propertyTypeIds.Contains(r.PropertyTypeId))
                .Where(l => l.UserId == userId)
                .GroupBy(r => r.PropertyTypeId)
                .Select(r => new TypeCountModelDTO
                {
                    PropertyTypeId = r.Key,
                    Count = r.Count()
                })
                .ToList();
        }

        public List<PropertyStatusCountDto> GetPropertyStatusesOfListings(string userId)
        {
            return _context.PropertyListings
                .Where(l => l.UserId == userId)
                .Include(l=>l.PropertyStatus)
                .GroupBy(l => l.PropertyStatus)
                .Select(l => new PropertyStatusCountDto
                {
                    PropertyStatusId = l.Key.Id,
                    PropertyStatus = l.Key.Name,
                    RgbColor = l.Key.RgbColorForStatistics,
                    Count = l.Count()
                })
                .ToList();
        }

        public List<PropertyStatusCountDto> GetPropertyStatusesOfRequests(string userId)
        {
            return _context.PropertyRequests
                .Where(l => l.UserId == userId)
                .Include(l => l.PropertyStatus)
                .GroupBy(l => l.PropertyStatus)
                .Select(l => new PropertyStatusCountDto
                {
                    PropertyStatusId = l.Key.Id,
                    PropertyStatus = l.Key.Name,
                    RgbColor = l.Key.RgbColorForStatistics,
                    Count = l.Count()
                })
                .ToList();
        }

        public FinalizedDto GetFinalizedListings(string userId)
        {
            var finalized = _context.PropertyListings
                .Where(l=>l.UserId == userId)
                .Where(l => l.DealStatus)
                .Count();

            var notFinalized = _context.PropertyListings
                .Where(l => l.UserId == userId)
                .Where(l => !l.DealStatus)
                .Count();

            return new(finalized, notFinalized);
        }

        public FinalizedDto GetFinalizedRequests(string userId)
        {
            var finalized = _context.PropertyRequests
                .Where(l => l.UserId == userId)
                .Where(r => r.DealStatus)
                .Count();

            var notFinalized = _context.PropertyRequests
                .Where(l => l.UserId == userId)
                .Where(r => !r.DealStatus)
                .Count();

            return new(finalized, notFinalized);
        }
    }
}

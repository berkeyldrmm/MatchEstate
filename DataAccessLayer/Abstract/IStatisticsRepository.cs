using Shared.Dtos.Statistics;

namespace DataAccessLayer.Abstract
{
    public interface IStatisticsRepository
    {
        public List<PropertyStatusCountDto> GetPropertyStatusesOfListings(string userId);
        public List<PropertyStatusCountDto> GetPropertyStatusesOfRequests(string userId);
        public List<TypeCountModelDTO> GetPropertyTypesOfListings(string userId);
        public List<TypeCountModelDTO> GetPropertyTypesOfRequests(string userId);
        public FinalizedDto GetFinalizedListings(string userId);
        public FinalizedDto GetFinalizedRequests(string userId);
    }
}

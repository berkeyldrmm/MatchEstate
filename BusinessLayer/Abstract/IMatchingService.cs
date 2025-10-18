using Shared.Dtos.PropertyListing;
using Shared.Dtos.PropertyRequest;

namespace BusinessLayer.Abstract
{
    public interface IMatchingService
    {
        public Task<List<(PropertyRequestCardDto request, PropertyListingCardDto listing)>> FindMatches(string userId);
    }
}

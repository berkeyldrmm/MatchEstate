using BusinessLayer.Abstract;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class MatchingService : IMatchingService
    {
        private readonly IPropertyListingService _listingService;
        private readonly IPropertyRequestService _requestService;
        public MatchingService(IPropertyListingService listingService, IPropertyRequestService requestService)
        {
            _listingService = listingService;
            _requestService = requestService;
        }

        public async Task<List<(PropertyRequest request, PropertyListing listing)>> FindMatches(string userId)
        {
            IEnumerable<PropertyRequest> requests = await _requestService.GetAllWithClient(userId);
            List<(PropertyRequest request, PropertyListing listing)> requestListingPairs = new List<(PropertyRequest request, PropertyListing listing)>();
            foreach (var request in requests)
            {
                List<PropertyListing> listings = await _listingService.GetListingsForRequest(userId, request);

                foreach (var listing in listings)
                {
                    requestListingPairs.Add((request, listing));
                }
            }

            return requestListingPairs;
        }
    }
}

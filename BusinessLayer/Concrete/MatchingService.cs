using BusinessLayer.Abstract;
using EntityLayer.Entities;
using Shared.Dtos.PropertyListing;
using Shared.Dtos.PropertyRequest;
using Shared.Services;
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

        public async Task<List<(PropertyRequestCardDto request, PropertyListingCardDto listing)>> FindMatches(string userId)
        {
            IEnumerable<PropertyRequest> requests = await _requestService.GetAllWithClient(userId);
            var requestListingPairs = new List<(PropertyRequestCardDto request, PropertyListingCardDto listing)>();

            foreach (var request in requests)
            {
                var listings = await _listingService.GetListingsForRequest(userId, request);

                foreach (var listing in listings)
                {
                    var requestDto = new PropertyRequestCardDto()
                    {
                        Id = request.Id,
                        Title = request.Title,
                        ClientNameSurname = request.Client.NameSurname,
                        MinPrice = request.MinimumPrice.ToString(),
                        MaxPrice = request.MaximumPrice.ToString(),
                        City = request.City,
                        PropertyStatus = request.PropertyStatus.Name,
                        PropertyType = request.PropertyType.PropertyName,
                        AddedDate = request.AddedDate.Write()
                    };

                    requestListingPairs.Add((requestDto, listing));
                }
            }

            return requestListingPairs;
        }
    }
}

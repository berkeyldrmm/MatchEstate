using BusinessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.PropertyRequest;
using System.Security.Claims;

namespace RealEstate.ViewComponents
{
    public class GetRequestsOfListingComponent : ViewComponent
    {
        private readonly IPropertyRequestService _requestService;
        private readonly IPropertyListingService _listingService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetRequestsOfListingComponent(IPropertyRequestService requestService, IPropertyListingService listingService, IHttpContextAccessor httpContextAccessor)
        {
            _requestService = requestService;
            _listingService = listingService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync(string listingId)
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            PropertyListing listing = await _listingService.GetWithClient(userId, listingId);
            List<PropertyRequestCardDto> requests = await _requestService.GetRequestsForListing(userId, listing);
            return View(requests);
        }
    }
}

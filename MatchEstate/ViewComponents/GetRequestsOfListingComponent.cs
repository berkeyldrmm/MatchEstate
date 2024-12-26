using BusinessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RealEstate.ViewComponents
{
    public class GetRequestsOfListingComponent : ViewComponent
    {
        private readonly IRequestService _requestService;
        private readonly IListingService _listingService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetRequestsOfListingComponent(IRequestService requestService, IListingService listingService, IHttpContextAccessor httpContextAccessor)
        {
            _requestService = requestService;
            _listingService = listingService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync(string listingId)
        {
            PropertyListing listing = await _listingService.GetOne(listingId);
            List<PropertyRequest> requests = await _requestService.GetRequestsForListing(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, listing);
            return View(requests);
        }
    }
}

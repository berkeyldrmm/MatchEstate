using BusinessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.PropertyListing;
using System.Security.Claims;

namespace RealEstate.ViewComponents
{
    public class GetListingsOfRequestComponent : ViewComponent
    {
        private readonly IPropertyListingService _listingService;
        private readonly IPropertyRequestService _requestService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetListingsOfRequestComponent(IPropertyListingService listingService, IPropertyRequestService requestService, IHttpContextAccessor httpContextAccessor)
        {
            _listingService = listingService;
            _requestService = requestService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync(string requestId)
        {
            string userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            PropertyRequest request = await _requestService.GetWithClient(userId, requestId);
            List<PropertyListingCardDto> listings = await _listingService.GetListingsForRequest(userId, request);
            return View(listings);
        }
    }
}

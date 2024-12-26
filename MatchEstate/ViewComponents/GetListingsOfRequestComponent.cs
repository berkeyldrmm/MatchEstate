using BusinessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RealEstate.ViewComponents
{
    public class GetListingsOfRequestComponent : ViewComponent
    {
        private readonly IListingService _listingService;
        private readonly IRequestService _requestService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetListingsOfRequestComponent(IListingService listingService, IRequestService requestService, IHttpContextAccessor httpContextAccessor)
        {
            _listingService = listingService;
            _requestService = requestService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync(string requestId)
        {
            PropertyRequest request = await _requestService.GetOne(requestId);
            List<PropertyListing> listings = await _listingService.GetListingsForRequest(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, request);
            return View(listings);
        }
    }
}

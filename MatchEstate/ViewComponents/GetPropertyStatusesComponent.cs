using BusinessLayer.Abstract;
using MatchEstate.Models;
using Microsoft.AspNetCore.Mvc;

namespace MatchEstate.ViewComponents
{
    public class GetPropertyStatusesComponent : ViewComponent
    {
        private readonly IPropertyStatusService _propertyStatusService;

        public GetPropertyStatusesComponent(IPropertyStatusService propertyStatusService)
        {
            _propertyStatusService = propertyStatusService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string view, string bgColor, int propertyStatusId = 1)
        {
            var propertyStatuses = await _propertyStatusService.GetAll();

            var propertyStatusViewModel = new PropertyStatusViewModel
            {
                PropertyStatuses = propertyStatuses,
                PropertyStatusId = propertyStatusId
            };
            ViewData["bgColor"] = bgColor;
            return View(view, propertyStatusViewModel);
        }
    }
}

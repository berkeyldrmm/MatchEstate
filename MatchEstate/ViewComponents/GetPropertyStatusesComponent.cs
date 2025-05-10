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

        public async Task<IViewComponentResult> InvokeAsync(string view)
        {
            var propertyStatuses = await _propertyStatusService.GetAll();

            var propertyStatusViewModel = new PropertyStatusViewModel
            {
                PropertyStatuses = propertyStatuses,
                PropertyStatusId = 1
            };

            return View(view, propertyStatusViewModel);
        }
    }
}

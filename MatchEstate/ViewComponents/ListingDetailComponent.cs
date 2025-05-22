using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.Abstractions;
using Shared.Dtos.PropertyListing.Detail;

namespace MatchEstate.ViewComponents
{
    public class ListingDetailComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IPropertyListingDetailDto dto)
        {
            string view = "";
            switch (dto.PropertyTypeId)
            {
                case 1:
                    view = "Shop";
                    if(dto is ShopListingDetailDto)
                        return View(view, dto);
                    break;
                case 2:
                    view = "Land";
                    if (dto is LandListingDetailDto)
                        return View(view, dto);
                    break;
                case 3:
                    view = "CommercialUnit";
                    if (dto is CommercialUnitListingDetailDto)
                        return View(view, dto);
                    break;
                case 4:
                    view = "Farmland";
                    if (dto is FarmlandListingDetailDto)
                        return View(view, dto);
                    break;
                case 5:
                    view = "Apartment";
                    if (dto is ApartmentListingDetailDto)
                        return View(view, dto);
                    break;
                default:
                    break;
            }

            return Content("Invalid model cast for Apartment");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.Abstractions;
using Shared.Dtos.PropertyListing.Detail;

namespace MatchEstate.ViewComponents
{
    public class ListingDetailComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IPropertyListingDetailDto dto)
        {
            switch (dto.PropertyTypeId)
            {
                case 1:
                    if(dto is ShopListingDetailDto)
                        return View("Shop", dto);
                    break;
                case 2:
                    if (dto is LandListingDetailDto)
                        return View("Land", dto);
                    break;
                case 3:
                    if (dto is CommercialUnitListingDetailDto)
                        return View("CommercialUnit", dto);
                    break;
                case 4:
                    if (dto is ApartmentListingDetailDto)
                        return View("Apartment", dto);
                    break;
                case 5:
                    if (dto is FarmlandListingDetailDto)
                        return View("Farmland", dto);
                    break;
                default:
                    break;
            }

            return Content("Invalid model cast for Apartment");
        }
    }
}

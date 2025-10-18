using Shared.Dtos.Abstractions;

namespace Shared.Dtos.PropertyListing.Detail
{
    public class LandListingDetailDto : PropertyListingDetailDto, IPropertyListingDetailDto
    {
        public string SquareMetersize { get; set; }
        public string BlockNumber { get; set; }
        public string ParselNumber { get; set; }
        public string SheetNumber { get; set; }
        public bool ZoningStatus { get; set; }
        public bool TitleSheetState { get; set; }
        public bool LandShareEligibity { get; set; }
        public string PricePerSquareMetersize { get; set; }
    }
}

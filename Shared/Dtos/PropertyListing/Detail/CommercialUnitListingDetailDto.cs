using Shared.Dtos.Abstractions;

namespace Shared.Dtos.PropertyListing.Detail
{
    public class CommercialUnitListingDetailDto : PropertyListingDetailDto, IPropertyListingDetailDto
    {
        public string SquareMetersize { get; set; }
        public string BlockNumber { get; set; }
        public string ParselNumber { get; set; }
        public string PricePerSquareMetersize { get; set; }
    }
}

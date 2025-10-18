using Shared.Dtos.Abstractions;

namespace Shared.Dtos.PropertyListing.Detail
{
    public class ShopListingDetailDto : PropertyListingDetailDto, IPropertyListingDetailDto
    {
        public string GrossSquareMetersize { get; set; }
        public string SquareMetersize { get; set; }
        public string BlockNumber { get; set; }
        public string ParselNumber { get; set; }
        public string NumberOfRooms { get; set; }
        public string NumberOfFloor { get; set; }
        public string Floor { get; set; }
        public bool Elevator { get; set; }
        public string UsageState { get; set; }
        public string Dues { get; set; }
        public string AgeOfBuilding { get; set; }
        public string HeatingType { get; set; }
        public bool EligibityForLoan { get; set; }
        public bool ParkingLot { get; set; }
        public bool Furnished { get; set; }
        public string PricePerSquareMetersize { get; set; }
    }
}

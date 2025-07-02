using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.PropertyListing
{
    public class AddListingDTO
    {
        public string RadioForClient { get; set; }
        public string? ClientId { get; set; } = string.Empty;
        public string? ListingTitle { get; set; } = string.Empty;
        public string? ClientNameSurname { get; set; } = string.Empty;
        public string? ClientEmail { get; set; } = string.Empty;
        public string? ClientPhoneNumber { get; set; } = string.Empty;
        public int PropertyTypeId { get; set; }
        public string SquareMetersize { get; set; } = string.Empty;
        public string? SquareMetersizeGross { get; set; } = string.Empty;
        public string? PricePerSquareMetersize { get; set; } = string.Empty;
        public string? BlockNumber { get; set; } = string.Empty;
        public string? SheetNumber { get; set; } = string.Empty;
        public string? ParselNumber { get; set; } = string.Empty;
        public string? ZoningStatus { get; set; } = string.Empty;
        public string? LandShareEligibity { get; set; } = string.Empty;
        public string? IsEligibleForLoan { get; set; } = string.Empty;
        public string? NumberOfRooms { get; set; } = string.Empty;
        public string? AgeOfBuilding { get; set; } = string.Empty;
        public string? Floor { get; set; } = string.Empty;
        public string? NumberOfFloor { get; set; } = string.Empty;
        public string? HeatingType { get; set; } = string.Empty;
        public string? NumberOfBathrooms { get; set; } = string.Empty;
        public string? NumberOfBalcony { get; set; } = string.Empty;
        public string? ParkingLot { get; set; } = string.Empty;
        public string? Elevator { get; set; } = string.Empty;
        public string? Furnished { get; set; } = string.Empty;
        public string? UsageState { get; set; } = string.Empty;
        public string? IsInResidentalComplex { get; set; } = string.Empty;
        public string? Dues { get; set; } = string.Empty;
        public string? TitleSheetState { get; set; } = string.Empty;
        public int PropertyStatusId { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Neighbourhood { get; set; }
        public decimal Price { get; set; }
        public string? Commission { get; set; } = string.Empty;
        public string? Details { get; set; } = string.Empty;
        public string? ImageBase64 { get; set; } = string.Empty;
    }
}

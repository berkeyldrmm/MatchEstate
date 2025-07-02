using EntityLayer.Entities;
using Shared.Dtos.PropertyListing;

namespace BusinessLayer.Mapping
{
    public static class ListingMapper
    {
        public static PropertyListing MapToListingEntity(AddListingDTO dto, string userId, string propertyId)
        {
            PropertyListing listing = new PropertyListing()
            {
                Id = propertyId,
                Title = dto.ListingTitle,
                Price = dto.Price,
                PropertyStatusId = Convert.ToInt32(dto.PropertyStatusId),
                DealStatus = false,
                UserId = userId,
                City = dto.City,
                District = dto.District,
                Neighbourhood = dto.Neighbourhood,
                ImageBase64 = dto.ImageBase64
            };
            listing.PropertyTypeId = Convert.ToInt32(dto.PropertyTypeId);
            if (dto.RadioForClient == "1")
            {
                var client = new Client()
                {
                    Id = Guid.NewGuid().ToString(),
                    NameSurname = dto.ClientNameSurname,
                    Email = dto.ClientEmail,
                    PhoneNumber = dto.ClientPhoneNumber,
                    UserId = userId
                };

                listing.ClientId = client.Id.ToString();
                listing.Client = client;
            }
            else
            {
                listing.ClientId = dto.ClientId;
            }
            listing.AddedDate = DateTime.Now;

            listing.Commission = Convert.ToDecimal(dto.Commission);

            listing.Details = dto.Details;

            return listing;
        }

        public static UpdateListingDto MapToUpdateListingDto(PropertyListing listing)
        {
            return new UpdateListingDto
            {
                ListingId = listing.Id,
                ListingTitle = listing.Title,
                PropertyTypeId = listing.PropertyTypeId,
                Price = listing.Price.ToString().Split(",")[0],
                City = listing.City,
                District = listing.District,
                Neighbourhood = listing.Neighbourhood,
                PropertyStatusId = listing.PropertyStatusId,
                Details = listing.Details,
                ClientId = listing.ClientId,
                ClientNameSurname = "",
                ClientEmail = "",
                ClientPhoneNumber = "",
                Commission = listing.Commission.ToString().Split(",")[0],
                RadioForClient = "0",
            };
        }

        public static PropertyListing MapToPropertyListing(UpdateListingDto dto, PropertyListing listing, string userId)
        {

            if (dto.RadioForClient == "1")
            {
                var client = new Client()
                {
                    Id = Guid.NewGuid().ToString(),
                    NameSurname = dto.ClientNameSurname,
                    Email = dto.ClientEmail,
                    PhoneNumber = dto.ClientPhoneNumber,
                    UserId = userId
                };

                listing.ClientId = client.Id;
                listing.Client = client;
            }
            else
            {
                listing.ClientId = dto.ClientId;
            }
            listing.Title = dto.ListingTitle;
            listing.Price = Convert.ToDecimal(dto.Price);
            listing.PropertyStatusId = dto.PropertyStatusId;
            listing.City = dto.City;
            listing.District = dto.District;
            listing.Neighbourhood = dto.Neighbourhood;
            listing.Details = dto.Details;

            
            listing.Commission = Convert.ToDecimal(dto.Commission);

            return listing;
        }

        public static Property MapToProperty(AddListingDTO dto)
        {
            var property = new Property();
            switch (dto.PropertyTypeId)
            {
                case 1:

                    property = new Shop()
                    {
                        BlockNumber = dto.BlockNumber,
                        ParselNumber = dto.ParselNumber,
                        Dues = Convert.ToInt32(dto.Dues),
                        HasElevator = Convert.ToBoolean(Convert.ToInt32(dto.Elevator)),
                        HasParkingLot = Convert.ToBoolean(Convert.ToInt32(dto.ParkingLot)),
                        AgeOfBuilding = dto.AgeOfBuilding,
                        Floor = Convert.ToInt32(dto.Floor),
                        IsFurnished = Convert.ToBoolean(Convert.ToInt32(dto.Furnished)),
                        HeatingType = dto.HeatingType,
                        NumberOfFloors = Convert.ToInt32(dto.NumberOfFloor),
                        IsEligibleForLoan = Convert.ToBoolean(Convert.ToInt32(dto.IsEligibleForLoan)),
                        UsageState = dto.UsageState,
                        SquareMeterSizeGross = Convert.ToInt32(dto.SquareMetersizeGross),
                        NumberOfRooms = dto.NumberOfRooms
                    };

                    break;
                case 2:
                    property = new Land()
                    {
                        BlockNumber = dto.BlockNumber,
                        ParselNumber = dto.ParselNumber,
                        ZoningStatus = Convert.ToBoolean(Convert.ToInt32(dto.ZoningStatus)),
                        LandShareEligibility = Convert.ToBoolean(Convert.ToInt32(dto.IsEligibleForLoan)),
                        SheetNumber = dto.SheetNumber,
                        TitleSheetState = Convert.ToBoolean(Convert.ToInt32(dto.TitleSheetState))
                    };
                    break;
                case 3:
                    property = new CommercialUnit()
                    {
                        BlockNumber = dto.BlockNumber,
                        ParselNumber = dto.ParselNumber
                    };
                    break;
                case 4:
                    property = new Farmland()
                    {
                        BlockNumber = dto.BlockNumber,
                        ParselNumber = dto.ParselNumber,
                        SheetNumber = dto.SheetNumber,
                        TitleDeedState = Convert.ToBoolean(Convert.ToInt32(dto.TitleSheetState)),
                        ZoningStatus = Convert.ToBoolean(Convert.ToInt32(dto.ZoningStatus))
                    };
                    break;
                case 5:
                    property = new Apartment()
                    {
                        BlockNumber = dto.BlockNumber,
                        Dues = Convert.ToInt32(dto.Dues),
                        HasElevator = Convert.ToBoolean(Convert.ToInt32(dto.Elevator)),
                        HasParkingLot = Convert.ToBoolean(Convert.ToInt32(dto.ParkingLot)),
                        AgeOfBuilding = dto.AgeOfBuilding,
                        Floor = Convert.ToInt32(dto.Floor),
                        IsFurnished = Convert.ToBoolean(Convert.ToInt32(dto.Furnished)),
                        HeatingType = dto.HeatingType,
                        NumberOfFloors = Convert.ToInt32(dto.NumberOfFloor),
                        IsEligibleForLoan = Convert.ToBoolean(Convert.ToInt32(dto.IsEligibleForLoan)),
                        UsageState = dto.UsageState,
                        SquareMeterSizeGross = Convert.ToInt32(dto.SquareMetersizeGross),
                        ParselNumber = dto.ParselNumber,
                        NumberOfRooms = dto.NumberOfRooms,
                        IsInResidentalComplex = Convert.ToBoolean(Convert.ToInt32(dto.IsInResidentalComplex)),
                        NumberOfBalcony = Convert.ToInt32(dto.NumberOfBalcony),
                        NumberOfBathRooms = Convert.ToInt32(dto.NumberOfBathrooms)
                    };
                    break;
                default:
                    break;
            }

            property.Id = Guid.NewGuid().ToString();
            property.ListingId = Guid.NewGuid().ToString();
            property.SquareMeterSize = dto.SquareMetersize;
            property.PricePerSquareMeter = Math.Ceiling(dto.Price / Convert.ToInt32(property.SquareMeterSize));

            return property;
        }
    }

}

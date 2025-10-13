using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shared.Dtos.Abstractions;
using Shared.Dtos.PropertyListing;
using Shared.Dtos.PropertyListing.Detail;
using Shared.Services;

namespace BusinessLayer.Mapping
{
    public static class ListingMapper
    {
        public static PropertyListing MapToListingEntity(AddListingDTO dto, string userId, string listingId, List<string> images)
        {
            PropertyListing listing = new PropertyListing()
            {
                Id = listingId,
                Title = dto.ListingTitle,
                Price = dto.Price,
                PropertyStatusId = Convert.ToInt32(dto.PropertyStatusId),
                DealStatus = false,
                UserId = userId,
                City = dto.City,
                District = dto.District,
                Neighbourhood = dto.Neighbourhood,
                LocationUrl = dto.LocationUrl,
                Images = JsonConvert.SerializeObject(images)
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
                LocationUrl = listing.LocationUrl,
                ExistingImages = JsonConvert.DeserializeObject<List<string>>(listing.Images) ?? []
            };
        }

        public static PropertyListing MapToPropertyListing(UpdateListingDto dto, PropertyListing listing, string userId, List<string> newImageIds)
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
            listing.LocationUrl = dto.LocationUrl;

            var existingImages = JsonConvert.DeserializeObject<List<string>>(listing.Images) ?? new List<string>();
            foreach (var image in dto.DeletingImages)
            {
                existingImages.Remove(image);
            }

            foreach (var id in newImageIds)
            {
                existingImages.Add(id);
            }

            listing.Images = JsonConvert.SerializeObject(existingImages);

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
                case 5:
                    property = new Farmland()
                    {
                        BlockNumber = dto.BlockNumber,
                        ParselNumber = dto.ParselNumber,
                        SheetNumber = dto.SheetNumber,
                        TitleDeedState = Convert.ToBoolean(Convert.ToInt32(dto.TitleSheetState)),
                        ZoningStatus = Convert.ToBoolean(Convert.ToInt32(dto.ZoningStatus))
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

        public static async Task<IPropertyListingDetailDto> MapToDetailDto(int propertyId, IQueryable<PropertyListing> query)
        {
            PropertyListingDetailDto dto = new();

            switch (propertyId)
            {
                case 1:
                    dto = await query.Select(l => new ShopListingDetailDto
                    {
                        Id = l.Id,
                        AddedDate = l.AddedDate.Write(),
                        Address = $"{l.Neighbourhood}, {l.District}, {l.City}",
                        Title = l.Title,
                        ClientNameSurname = l.Client.NameSurname,
                        ClientEmailAddress = l.Client.Email,
                        ClientPhoneNumber = l.Client.PhoneNumber,
                        Price = l.Price.ToString(),
                        PropertyStatus = l.PropertyStatus.Name,
                        PropertyTypeId = l.PropertyTypeId,
                        PropertyType = l.PropertyType.PropertyName,
                        Details = l.Details,
                        Commission = l.Commission.ToString(),
                        DealStatus = l.DealStatus,
                        Earning = l.Earning.ToString(),
                        Images = l.Images,
                        AgeOfBuilding = l.Shop.AgeOfBuilding,
                        BlockNumber = l.Shop.BlockNumber,
                        Dues = l.Shop.Dues.ToString(),
                        Elevator = l.Shop.HasElevator,
                        EligibityForLoan = l.Shop.IsEligibleForLoan,
                        Floor = l.Shop.Floor.ToString(),
                        Furnished = l.Shop.IsFurnished,
                        GrossSquareMetersize = l.Shop.SquareMeterSizeGross.ToString(),
                        HeatingType = l.Shop.HeatingType,
                        NumberOfFloor = l.Shop.NumberOfFloors.ToString(),
                        NumberOfRooms = l.Shop.NumberOfRooms,
                        ParkingLot = l.Shop.HasParkingLot,
                        ParselNumber = l.Shop.ParselNumber,
                        PricePerSquareMetersize = l.Shop.PricePerSquareMeter.ToString(),
                        SquareMetersize = l.Shop.SquareMeterSize,
                        UsageState = l.Shop.UsageState
                    }).FirstOrDefaultAsync();
                    break;
                case 2:
                    dto = await query.Select(l => new LandListingDetailDto
                    {
                        Id = l.Id,
                        AddedDate = l.AddedDate.Write(),
                        Address = $"{l.Neighbourhood}, {l.District}, {l.City}",
                        Title = l.Title,
                        ClientNameSurname = l.Client.NameSurname,
                        ClientEmailAddress = l.Client.Email,
                        ClientPhoneNumber = l.Client.PhoneNumber,
                        Price = l.Price.ToString(),
                        PropertyStatus = l.PropertyStatus.Name,
                        PropertyTypeId = l.PropertyTypeId,
                        PropertyType = l.PropertyType.PropertyName,
                        Details = l.Details,
                        Commission = l.Commission.ToString(),
                        Earning = l.Earning.ToString(),
                        DealStatus = l.DealStatus,
                        Images = l.Images,
                        BlockNumber = l.Land.BlockNumber,
                        LandShareEligibity = l.Land.LandShareEligibility,
                        ParselNumber = l.Land.ParselNumber,
                        PricePerSquareMetersize = l.Land.PricePerSquareMeter.ToString(),
                        SheetNumber = l.Land.SheetNumber,
                        SquareMetersize = l.Land.SquareMeterSize,
                        ZoningStatus = l.Land.ZoningStatus
                    }).FirstOrDefaultAsync();
                    break;
                case 3:
                    dto = await query.Select(l => new CommercialUnitListingDetailDto
                    {
                        Id = l.Id,
                        AddedDate = l.AddedDate.Write(),
                        Address = $"{l.Neighbourhood}, {l.District}, {l.City}",
                        Title = l.Title,
                        ClientNameSurname = l.Client.NameSurname,
                        ClientEmailAddress = l.Client.Email,
                        ClientPhoneNumber = l.Client.PhoneNumber,
                        Price = l.Price.ToString(),
                        PropertyStatus = l.PropertyStatus.Name,
                        PropertyTypeId = l.PropertyTypeId,
                        PropertyType = l.PropertyType.PropertyName,
                        Details = l.Details,
                        Commission = l.Commission.ToString(),
                        Earning = l.Earning.ToString(),
                        DealStatus = l.DealStatus,
                        Images = l.Images,
                        BlockNumber = l.CommercialUnit.BlockNumber,
                        ParselNumber = l.CommercialUnit.ParselNumber,
                        SquareMetersize = l.CommercialUnit.SquareMeterSize,
                        PricePerSquareMetersize = l.CommercialUnit.PricePerSquareMeter.ToString()
                    }).FirstOrDefaultAsync();
                    break;
                case 4:
                    dto = await query.Select(l => new ApartmentListingDetailDto
                    {
                        Id = l.Id,
                        AddedDate = l.AddedDate.Write(),
                        Address = $"{l.Neighbourhood}, {l.District}, {l.City}",
                        Title = l.Title,
                        ClientNameSurname = l.Client.NameSurname,
                        ClientEmailAddress = l.Client.Email,
                        ClientPhoneNumber = l.Client.PhoneNumber,
                        Price = l.Price.ToString(),
                        PropertyStatus = l.PropertyStatus.Name,
                        PropertyTypeId = l.PropertyTypeId,
                        PropertyType = l.PropertyType.PropertyName,
                        Commission = l.Commission.ToString(),
                        Earning = l.Earning.ToString(),
                        DealStatus = l.DealStatus,
                        Details = l.Details,
                        Images = l.Images,
                        AgeOfBuilding = l.Apartment.AgeOfBuilding,
                        BlockNumber = l.Apartment.BlockNumber,
                        Dues = l.Apartment.Dues.ToString(),
                        Elevator = l.Apartment.HasElevator,
                        EligibityForLoan = l.Apartment.IsEligibleForLoan,
                        Floor = l.Apartment.Floor.ToString(),
                        Furnished = l.Apartment.IsFurnished,
                        GrossSquareMetersize = l.Apartment.SquareMeterSizeGross.ToString(),
                        HeatingType = l.Apartment.HeatingType,
                        NumberOfFloor = l.Apartment.NumberOfFloors.ToString(),
                        NumberOfRooms = l.Apartment.NumberOfRooms,
                        ParkingLot = l.Apartment.HasParkingLot,
                        ParselNumber = l.Apartment.ParselNumber,
                        PricePerSquareMetersize = l.Apartment.PricePerSquareMeter.ToString(),
                        SquareMetersize = l.Apartment.SquareMeterSize,
                        IsResidentalComplex = l.Apartment.IsInResidentalComplex,
                        NumberOfBalcony = l.Apartment.NumberOfBalcony.ToString(),
                        NumberOfBathrooms = l.Apartment.NumberOfBathRooms.ToString(),
                        UsageState = l.Apartment.UsageState
                    }).FirstOrDefaultAsync();
                    break;
                case 5:
                    dto = await query.Select(l => new FarmlandListingDetailDto
                    {
                        Id = l.Id,
                        AddedDate = l.AddedDate.Write(),
                        Address = $"{l.Neighbourhood}, {l.District}, {l.City}",
                        Title = l.Title,
                        ClientNameSurname = l.Client.NameSurname,
                        ClientEmailAddress = l.Client.Email,
                        ClientPhoneNumber = l.Client.PhoneNumber,
                        Price = l.Price.ToString(),
                        PropertyStatus = l.PropertyStatus.Name,
                        PropertyTypeId = l.PropertyTypeId,
                        PropertyType = l.PropertyType.PropertyName,
                        Details = l.Details,
                        Commission = l.Commission.ToString(),
                        Earning = l.Earning.ToString(),
                        DealStatus = l.DealStatus,
                        Images = l.Images,
                        BlockNumber = l.Farmland.BlockNumber,
                        ParselNumber = l.Farmland.ParselNumber,
                        PricePerSquareMetersize = l.Farmland.PricePerSquareMeter.ToString(),
                        SheetNumber = l.Farmland.SheetNumber,
                        SquareMetersize = l.Farmland.SquareMeterSize,
                        TitleDeedState = l.Farmland.TitleDeedState,
                        ZoningStatus = l.Farmland.ZoningStatus
                    }).FirstOrDefaultAsync();
                    break;
                default:
                    break;
            }

            return dto;
        }
    }

}

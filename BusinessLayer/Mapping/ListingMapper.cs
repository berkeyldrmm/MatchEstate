using DTOLayer;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mapping
{
    public static class ListingMapper
    {
        //public static PropertyListing MapToPropertyListing(AddListingDTO dto, string userId)
        //{
        //    var listing = new PropertyListing
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        Title = dto.ListingTitle ?? string.Empty,
        //        PropertyTypeId = Convert.ToInt32(dto.PropertyType),
        //        Price = dto.Price,
        //        City = dto.City,
        //        District = dto.District,
        //        Neighbourhood = dto.Neighbourhood,
        //        UserId = userId,
        //        PropertyStatusId = dto.PropertyStatusId == "1" ? "For Sale" : "For Sale",
        //        Details = dto.Details,
        //        AddedDate = DateTime.Now,
        //    };

        //    if (dto.RadioForClient == "1")
        //    {
        //        var client = new Client
        //        {
        //            Id = Guid.NewGuid().ToString(),
        //            NameSurname = dto.ClientNameSurname ?? string.Empty,
        //            Email = dto.ClientEmail ?? string.Empty,
        //            PhoneNumber = dto.ClientPhoneNumber ?? string.Empty,
        //            UserId = userId
        //        };

        //        listing.ClientId = client.Id;
        //        listing.Client = client;
        //    }
        //    else
        //    {
        //        listing.ClientId = dto.ClientId;
        //    }

        //    return listing;
        //}

        public static AddListingDTO MapToAddListingDTO(PropertyListing listing)
        {
            var dto = new AddListingDTO
            {
                ListingTitle = listing.Title,
                PropertyType = listing.PropertyTypeId.ToString(),
                Price = listing.Price,
                City = listing.City,
                District = listing.District,
                Neighbourhood = listing.Neighbourhood,
                //PropertyStatusId = listing.PropertyStatusId == "For Sale" ? "1" : "0",
                Details = listing.Details,
                ClientId = listing.ClientId,
                ClientNameSurname = listing.Client?.NameSurname,
                ClientEmail = listing.Client?.Email,
                ClientPhoneNumber = listing.Client?.PhoneNumber,
                Commission = listing.Commission.ToString().Split(",")[0]
            };

            switch (listing.PropertyTypeId)
            {
                case 1:
                    var shop = listing.Shop;
                    if (shop != null)
                    {
                        dto.SquareMetersize = shop.SquareMeterSize;
                        dto.Dues = shop.Dues.ToString();
                        dto.Elevator = shop.HasElevator ? "1" : "0";
                        dto.ParkingLot = shop.HasParkingLot ? "1" : "0";
                        dto.AgeOfBuilding = shop.AgeOfBuilding;
                        dto.Floor = shop.Floor.ToString();
                        dto.Furnished = shop.IsFurnished ? "1" : "0";
                        dto.HeatingType = shop.HeatingType;
                        dto.NumberOfFloor = shop.NumberOfFloors.ToString();
                        dto.IsEligibleForLoan = shop.IsEligibleForLoan ? "1" : "0";
                        dto.UsageState = shop.UsageState;
                        dto.SquareMetersizeGross = shop.SquareMeterSizeGross.ToString();
                        dto.ParselNumber = shop.ParselNumber;
                        dto.NumberOfRooms = shop.NumberOfRooms;
                    }
                    break;

                case 2:
                    var land = listing.Land;
                    if (land != null)
                    {
                        dto.SquareMetersize = land.SquareMeterSize;
                        dto.BlockNumber = land.BlockNumber;
                        dto.ParselNumber = land.ParselNumber;
                        dto.ZoningStatus = land.ZoningStatus ? "1" : "0";
                        dto.LandShareEligibity = land.LandShareEligibility ? "1" : "0";
                        dto.SheetNumber = land.SheetNumber;
                        dto.TitleSheetState = land.TitleSheetState ? "1" : "0";
                    }
                    break;

                case 3:
                    var commercialUnit = listing.CommercialUnit;
                    if (commercialUnit != null)
                    {
                        dto.SquareMetersize = commercialUnit.SquareMeterSize;
                        dto.BlockNumber = commercialUnit.BlockNumber;
                        dto.ParselNumber = commercialUnit.ParselNumber;
                    }
                    break;

                case 4:
                    var farmland = listing.Farmland;
                    if (farmland != null)
                    {
                        dto.SquareMetersize = farmland.SquareMeterSize;
                        dto.BlockNumber = farmland.BlockNumber;
                        dto.ParselNumber = farmland.ParselNumber;
                        dto.SheetNumber = farmland.SheetNumber;
                        dto.TitleSheetState = farmland.TitleDeedState ? "1" : "0";
                        dto.ZoningStatus = farmland.ZoningStatus ? "1" : "0";
                    }
                    break;

                case 5:
                    var apartment = listing.Apartment;
                    if (apartment != null)
                    {
                        dto.SquareMetersize = apartment.SquareMeterSize;
                        dto.BlockNumber = apartment.BlockNumber;
                        dto.Dues = apartment.Dues.ToString();
                        dto.Elevator = Convert.ToInt32(apartment.HasElevator).ToString();
                        dto.ParkingLot = Convert.ToInt32(apartment.HasParkingLot).ToString();
                        dto.AgeOfBuilding = apartment.AgeOfBuilding ?? string.Empty;
                        dto.Floor = apartment.Floor.ToString();
                        dto.Furnished = Convert.ToInt32(apartment.IsFurnished).ToString();
                        dto.HeatingType = apartment.HeatingType ?? string.Empty;
                        dto.NumberOfFloor = apartment.NumberOfFloors.ToString();
                        dto.IsEligibleForLoan = Convert.ToInt32(apartment.IsEligibleForLoan).ToString();
                        dto.UsageState = apartment.UsageState ?? string.Empty;
                        dto.SquareMetersizeGross = apartment.SquareMeterSizeGross.ToString();
                        dto.ParselNumber = apartment.ParselNumber ?? string.Empty;
                        dto.NumberOfRooms = apartment.NumberOfRooms ?? string.Empty;
                        dto.IsInResidentalComplex = Convert.ToInt32(apartment.IsInResidentalComplex).ToString();
                        dto.NumberOfBalcony = apartment.NumberOfBalcony.ToString();
                        dto.NumberOfBathrooms = apartment.NumberOfBathRooms.ToString();
                    };
                    break;

                default:
                    break;
            }

            return dto;
        }

        //public static void MapPropertyDetails(AddListingDTO dto, PropertyListing listing)
        //{
        //    switch (listing.PropertyTypeId)
        //    {
        //        case 1:
        //            listing.Shop = new Shop
        //            {
        //                Dues = Convert.ToInt32(dto.Dues),
        //                HasElevator = Convert.ToBoolean(Convert.ToInt32(dto.Elevator)),
        //                HasParkingLot = Convert.ToBoolean(Convert.ToInt32(dto.ParkingLot)),
        //                AgeOfBuilding = dto.AgeOfBuilding ?? string.Empty,
        //                Floor = Convert.ToInt32(dto.Floor),
        //                IsFurnished = Convert.ToBoolean(Convert.ToInt32(dto.Furnished)),
        //                HeatingType = dto.HeatingType ?? string.Empty,
        //                NumberOfFloors = Convert.ToInt32(dto.NumberOfFloor),
        //                IsEligibleForLoan = Convert.ToBoolean(Convert.ToInt32(dto.IsEligibleForLoan)),
        //                UsageState = dto.UsageState ?? string.Empty,
        //                SquareMeterSizeGross = Convert.ToInt32(dto.SquareMetersizeGross),
        //                ParselNumber = dto.ParselNumber ?? string.Empty,
        //                NumberOfRooms = dto.NumberOfRooms ?? string.Empty
        //            };
        //            break;

        //        case 2:
        //            listing.Land = new Land
        //            {
        //                BlockNumber = dto.BlockNumber ?? string.Empty,
        //                ParselNumber = dto.ParselNumber ?? string.Empty,
        //                ZoningStatus = Convert.ToBoolean(Convert.ToInt32(dto.ZoningStatus)),
        //                LandShareEligibility = Convert.ToBoolean(Convert.ToInt32(dto.LandShareEligibity)),
        //                SheetNumber = dto.SheetNumber ?? string.Empty,
        //                TitleSheetState = Convert.ToBoolean(Convert.ToInt32(dto.TitleSheetState))
        //            };
        //            break;

        //        case 3:
        //            listing.CommercialUnit = new CommercialUnit
        //            {
        //                BlockNumber = dto.BlockNumber ?? string.Empty,
        //                ParselNumber = dto.ParselNumber ?? string.Empty
        //            };
        //            break;

        //        case 4:
        //            listing.Farmland = new Farmland
        //            {
        //                BlockNumber = dto.BlockNumber ?? string.Empty,
        //                ParselNumber = dto.ParselNumber ?? string.Empty,
        //                SheetNumber = dto.SheetNumber ?? string.Empty,
        //                TitleDeedState = Convert.ToBoolean(Convert.ToInt32(dto.TitleSheetState)),
        //                ZoningStatus = Convert.ToBoolean(Convert.ToInt32(dto.ZoningStatus))
        //            };
        //            break;

        //        case 5:
        //            listing.Apartment = new Apartment
        //            {
        //                BlockNumber = dto.BlockNumber ?? string.Empty,
        //                Dues = Convert.ToInt32(dto.Dues),
        //                HasElevator = Convert.ToBoolean(Convert.ToInt32(dto.Elevator)),
        //                HasParkingLot = Convert.ToBoolean(Convert.ToInt32(dto.ParkingLot)),
        //                AgeOfBuilding = dto.AgeOfBuilding ?? string.Empty,
        //                Floor = Convert.ToInt32(dto.Floor),
        //                IsFurnished = Convert.ToBoolean(Convert.ToInt32(dto.Furnished)),
        //                HeatingType = dto.HeatingType ?? string.Empty,
        //                NumberOfFloors = Convert.ToInt32(dto.NumberOfFloor),
        //                IsEligibleForLoan = Convert.ToBoolean(Convert.ToInt32(dto.IsEligibleForLoan)),
        //                UsageState = dto.UsageState ?? string.Empty,
        //                SquareMeterSizeGross = Convert.ToInt32(dto.SquareMetersizeGross),
        //                ParselNumber = dto.ParselNumber ?? string.Empty,
        //                NumberOfRooms = dto.NumberOfRooms ?? string.Empty,
        //                IsInResidentalComplex = Convert.ToBoolean(Convert.ToInt32(dto.IsInResidentalComplex)),
        //                NumberOfBalcony = Convert.ToInt32(dto.NumberOfBalcony),
        //                NumberOfBathRooms = Convert.ToInt32(dto.NumberOfBathrooms)
        //            };
        //            break;

        //        default:
        //            break;
        //    }
        //}
    }

}

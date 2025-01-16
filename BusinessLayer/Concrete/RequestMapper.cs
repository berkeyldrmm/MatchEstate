using DTOLayer;
using EntityLayer.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public static class RequestMapper
    {
        //public static PropertyRequest MapToPropertyRequest(RequestModelDTO dto, string userId)
        //{
        //    var request = new PropertyRequest
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        Title = dto.RequestTitle ?? string.Empty,
        //        PropertyTypeId = Convert.ToInt32(dto.PropertyType),
        //        MinimumPrice = dto.MinPrice ?? 0,
        //        MaximumPrice = dto.MaxPrice ?? 0,
        //        City = dto.City,
        //        District = JsonConvert.SerializeObject(dto.District),
        //        UserId = userId,
        //        IsForSaleOrRent = dto.IsForSaleOrRent == "1" ? "For Sale" : "For Sale",
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

        //        request.ClientId = client.Id;
        //        request.Client = client;
        //    }
        //    else
        //    {
        //        request.ClientId = dto.ClientId;
        //    }

        //    return request;
        //}

        public static RequestModelDTO MapToRequestModelDTO(PropertyRequest request)
        {
            var dto = new RequestModelDTO
            {
                RequestTitle = request.Title,
                PropertyType = request.PropertyTypeId.ToString(),
                MinPrice = request.MinimumPrice,
                MaxPrice = request.MaximumPrice,
                City = request.City,
                District = JsonConvert.DeserializeObject<List<string>>(request.District),
                IsForSaleOrRent = request.IsForSaleOrRent == "For Sale" ? "1" : "0",
                Details = request.Details,
                ClientId = request.ClientId,
                ClientNameSurname = request.Client?.NameSurname,
                ClientEmail = request.Client?.Email,
                ClientPhoneNumber = request.Client?.PhoneNumber,
                NumberOfRooms = JsonConvert.DeserializeObject<List<string>>(request.NumberOfRooms),

            };

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

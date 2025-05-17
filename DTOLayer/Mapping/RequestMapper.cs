using DTOLayer.Dtos;
using EntityLayer.Entities;
using Newtonsoft.Json;

namespace BusinessLayer.Mapping
{
    public static class RequestMapper
    {
        public static UpdateRequestDto MapToUpdateRequestDto(PropertyRequest request)
        {
            return new UpdateRequestDto
            {
                RequestId = request.Id,
                RequestTitle = request.Title,
                PropertyTypeId = request.PropertyTypeId,
                MinPrice = request.MinimumPrice.ToString().Split(",")[0],
                MaxPrice = request.MaximumPrice.ToString().Split(",")[0],
                City = request.City,
                District = JsonConvert.DeserializeObject<List<string>>(request.District),
                PropertyStatusId = request.PropertyStatusId,
                Details = request.Details,
                ClientId = request.ClientId,
                ClientNameSurname = "",
                ClientEmail = "",
                ClientPhoneNumber = "",
                RadioForClient = "0",
                NumberOfRooms = JsonConvert.DeserializeObject<List<string>>(request.NumberOfRooms),
            };
        }

        public static PropertyRequest MapToPropertyRequest(UpdateRequestDto dto, PropertyRequest request, string userId)
        {
            request.Title = dto.RequestTitle;
            request.MinimumPrice = Convert.ToDecimal(dto.MinPrice);
            request.MaximumPrice = Convert.ToDecimal(dto.MaxPrice);
            request.PropertyStatusId = dto.PropertyStatusId;
            request.City = dto.City;
            request.District = JsonConvert.SerializeObject(dto.District);
            request.Details = dto.Details;

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

                request.ClientId = client.Id;
                request.Client = client;
            }
            else
            {
                request.ClientId = dto.ClientId;
            }

            return request;
        }
    }
}

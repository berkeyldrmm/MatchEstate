using Azure;
using BusinessLayer.Abstract;
using BusinessLayer.Mapping;
using DataAccessLayer;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.PropertyRequest;

namespace BusinessLayer.Concrete
{
    public class PropertyRequestService : IPropertyRequestService
    {
        private readonly IPropertyRequestRepository _reqeustRepository;
        private readonly IClientService _clientService;
        private readonly IUnitOfWork _unitOfWork;
        public PropertyRequestService(IPropertyRequestRepository reqeustRepository, IClientService clientService, IUnitOfWork unitOfWork)
        {
            _reqeustRepository = reqeustRepository;
            _clientService = clientService;
            _unitOfWork = unitOfWork;
        }

        public void DeleteRange(string userId, IEnumerable<string> Ids)
        {
            var items = _reqeustRepository.GetRange(userId, Ids);
            this.DeleteRange(items);
        }

        public void DeleteRange(IEnumerable<PropertyRequest> requests)
        {
            _reqeustRepository.DeleteRange(requests);
        }

        public Task<IEnumerable<PropertyRequest>> GetAll()
        {
            return _reqeustRepository.ReadAll();
        }

        public async Task<IEnumerable<PropertyRequest>> GetAllWithClient(string userId)
        {
            return await _reqeustRepository.GetAllWithClient(userId);
        }

        public object GetCountsOfRequestTypes(string userId)
        {
            return _reqeustRepository.GetCountsOfRequestTypes(userId);
        }

        public Task<PropertyType> GetPropertyType(int id)
        {
            return _reqeustRepository.GetPropertyType(id);
        }

        public Task<PropertyRequest> GetOne(string id)
        {
            return _reqeustRepository.Read(id);
        }

        public object GetForSaleOrRent(string userId)
        {
            return _reqeustRepository.GetForSaleOrRent(userId);
        }

        public async Task<PropertyRequest> GetWithClient(string userId, string id)
        {
            return await _reqeustRepository.GetWithClient(userId, id).FirstOrDefaultAsync();
        }

        public UpdateRequestDto? GetRequestForUpdate(string userId, string id)
        {
            return _reqeustRepository.GetRequestForUpdate(userId, id);
        }

        public async Task<bool> Insert(string userId, AddRequestDto requestModel)
        {
            PropertyRequest request = new PropertyRequest()
            {
                Id = Guid.NewGuid().ToString(),
                District = JsonConvert.SerializeObject(requestModel.District),
                NumberOfRooms = JsonConvert.SerializeObject(requestModel.NumberOfRooms),
                PropertyStatusId = requestModel.PropertyStatusId,
                UserId = userId,
                MinimumPrice = requestModel.MinPrice ?? 0,
                MaximumPrice = requestModel.MaxPrice ?? 0,
                Title = requestModel.RequestTitle,
                City = requestModel.City
            };
            request.PropertyTypeId = Convert.ToInt32(requestModel.PropertyTypeId);
            request.PropertyType = await this.GetPropertyType(request.PropertyTypeId);
            if (requestModel.RadioForClient == "1")
            {
                var client = new Client()
                {
                    Id = Guid.NewGuid().ToString(),
                    NameSurname = requestModel.ClientNameSurname,
                    Email = requestModel.ClientEmail,
                    PhoneNumber = requestModel.ClientPhoneNumber,
                    UserId = userId
                };

                request.ClientId = client.Id;
                request.Client = client;
            }
            else
            {
                request.ClientId = requestModel.ClientId;
                request.Client = await _clientService.GetOne(requestModel.ClientId);
            }

            request.AddedDate = DateTime.Now;

            request.Details = requestModel.Details ?? "";
            var result = await _reqeustRepository.Insert(request);
            int saved = await _unitOfWork.SaveChanges();
            return result && saved > 0;
        }

        public (IEnumerable<RequestPageDTO>, int) GetByFilters(string userId, RequestGetByFiltersDTO getByFilers)
        {
            var expressions = new List<Expression<Func<PropertyRequest, bool>>>();

            if (getByFilers.PropertyStatusId != "0")
                expressions.Add(t => t.PropertyStatusId == Convert.ToInt32(getByFilers.PropertyStatusId));

            if (getByFilers.PropertyType != 0)
                expressions.Add(t => t.PropertyTypeId == getByFilers.PropertyType);

            if (getByFilers.Status != "-1")
                expressions.Add(r => r.DealStatus == Convert.ToBoolean(Convert.ToInt16(getByFilers.Status)));

            if (getByFilers.Search != null && getByFilers.Search.Length > 2)
            {
                expressions.Add(t => t.Title.Contains(getByFilers.Search) || t.City.Contains(getByFilers.Search) || t.Client.NameSurname.Contains(getByFilers.Search));
            }

            return _reqeustRepository.GetByFilters(userId, expressions, getByFilers.Sort, Convert.ToInt16(getByFilers.PageNumber), Convert.ToInt16(getByFilers.PageSize));
        }

        public async Task<List<PropertyRequestCardDto>> GetRequestsForListing(string userId, PropertyListing ilan)
        {
            List<Expression<Func<PropertyRequest, bool>>> expressions = new List<Expression<Func<PropertyRequest, bool>>>();
            expressions.Add(t => t.District.Contains("\""+ilan.District+"\"") && t.City == ilan.City);
            expressions.Add(t => t.MinimumPrice <= ilan.Price && t.MaximumPrice >= ilan.Price);

            if (ilan.PropertyTypeId == 5)
            {
                expressions.Add(t => t.NumberOfRooms.Contains("\""+ilan.Apartment.NumberOfRooms+"\""));
            }
            else if (ilan.PropertyTypeId == 1)
            {
                expressions.Add(t => t.NumberOfRooms.Contains("\"" + ilan.Shop.NumberOfRooms + "\""));
            }

            expressions.Add(t => t.PropertyStatusId == ilan.PropertyStatusId);

            expressions.Add(t => t.PropertyTypeId == ilan.PropertyTypeId);

            expressions.Add(t => t.Client.Id != ilan.Client.Id);

            expressions.Add(i => !i.DealStatus);

            return await _reqeustRepository.GetRequestsForListing(userId, expressions);
        }

        public async Task<bool> Update(string userId, UpdateRequestDto dto)
        {
            var request = await _reqeustRepository.GetWithClient(userId, dto.RequestId).FirstOrDefaultAsync();
            if (request == null)
            {
                return false;
            }

            RequestMapper.MapToPropertyRequest(dto, request, userId);

            var result = await _reqeustRepository.Update(request);
            int saved = await _unitOfWork.SaveChanges();

            return result && saved > 0;
        }

        public async Task<PropertyRequestDetailDto> GetRequestDetail(string userId, string id)
        {
            return await _reqeustRepository.GetRequestDetail(userId, id);
        }

        public async Task<List<GetRequestsByPropertyTypeDto>> GetRequestsByPropertyType(string userId, int propertyTypeId)
        {
            return await _reqeustRepository.GetRequestsByPropertyType(userId, propertyTypeId);
        }

        public async Task<bool> FinalizeRequest(string userId, string requestId)
        {
            return await _reqeustRepository.FinalizeRequest(userId, requestId);
        }

        public async Task<IEnumerable<PropertyRequest>> GetRequestsNotDeal(string userId)
        {
            return await _reqeustRepository.GetRequestsNotDeal(userId);
        }

        public async Task<IEnumerable<PropertyRequest>> GetRequestsByClient(string userId, string clientId)
        {
            return await _reqeustRepository.GetRequestsByClient(userId, clientId);
        }
    }
}

using DataAccessLayer;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.PropertyRequest;

namespace BusinessLayer.Abstract
{
    public interface IPropertyRequestService : IGenericService<PropertyRequest, string>
    {
        public Task<bool> Insert(string userId, AddRequestDto requestModel);
        public Task<bool> Update(string userId, UpdateRequestDto requestModel);
        public Task<IEnumerable<PropertyRequest>> GetAllWithClient(string userId);
        public Task<PropertyRequest> GetWithClient(string userId, string id);
        public Task<PropertyType> GetPropertyType(int id);
        void DeleteRange(string userId, IEnumerable<string> Ids);
        void DeleteRange(IEnumerable<PropertyRequest> requests);
        public object GetCountsOfRequestTypes(string userId);
        public object GetForSaleOrRent(string userId);
        public Task<List<PropertyRequestCardDto>> GetRequestsForListing(string userId, PropertyListing listing);
        public (IEnumerable<RequestPageDTO>, int) GetByFilters(string userId, RequestGetByFiltersDTO getByFilters);
        public UpdateRequestDto? GetRequestForUpdate(string userId, string id);
        public Task<PropertyRequestDetailDto> GetRequestDetail(string userId, string id);
        public Task<List<GetRequestsByPropertyTypeDto>> GetRequestsByPropertyType(string userId, int propertyTypeId);
        public Task<bool> FinalizeRequest(string userId, string requestId);
        public Task<IEnumerable<PropertyRequest>> GetRequestsNotDeal(string userId);
        public Task<IEnumerable<PropertyRequest>> GetRequestsByClient(string userId, string clientId);
    }
}
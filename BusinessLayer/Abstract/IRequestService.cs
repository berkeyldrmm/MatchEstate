using DataAccessLayer;
using DTOLayer;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IRequestService : IGenericService<PropertyRequest>
    {
        public Task<(bool, string)> Insert(string userId, RequestModelDTO requestModel);
        public Task<IEnumerable<PropertyRequest>> GetAllWithClient(string userId);
        public Task<PropertyRequest> GetWithClient(string userId, string id);
        public Task<PropertyType> GetPropertyType(int id);
        void DeleteRange(string userId, IEnumerable<string> Ids);
        public object GetCountsOfRequestTypes(string userId);
        public object GetForSaleOrRent(string userId);
        public Task<List<PropertyRequest>> GetRequestsForListing(string userId, PropertyListing listing);
        public Task<IEnumerable<PropertyRequestDTO>> GetByFilters(string userId, RequestGetByFiltersDTO getByFilters);
    }
}
using DTOLayer;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IPropertyRequestRepository : IGenericRepository<PropertyRequest, string>
    {
        public Task<IEnumerable<PropertyRequest>> GetAllWithClient(string userId);
        public Task<PropertyRequest> GetWithClient(string userId, string id);
        public Task<PropertyType> GetPropertyType(int id);
        IEnumerable<PropertyRequest> GetRange(string userId, IEnumerable<string> Ids);
        public object GetCountsOfRequestTypes(string userId);
        public object GetForSaleOrRent(string userId);
        public Task<List<PropertyRequest>> GetRequestsForListing(string userId, List<Expression<Func<PropertyRequest, bool>>> expressions);
        public (IEnumerable<RequestPageDTO>, int) GetByFilters(string userId, List<Expression<Func<PropertyRequest, bool>>> expressions, string sort, int PageNumber, int PageSize);
        public int GetRequestCount(string userId);
    }
}

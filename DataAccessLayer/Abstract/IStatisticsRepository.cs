using DTOLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IStatisticsRepository
    {
        public List<PropertyStatusCountDto> GetPropertyStatusesOfListings(string userId);
        public List<PropertyStatusCountDto> GetPropertyStatusesOfRequests(string userId);
        public List<TypeCountModelDTO> GetPropertyTypesOfListings(string userId);
        public List<TypeCountModelDTO> GetPropertyTypesOfRequests(string userId);
    }
}

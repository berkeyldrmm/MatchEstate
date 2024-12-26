using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IMatchingService
    {
        public Task<List<(PropertyRequest request, PropertyListing listing)>> FindMatches(string userId);
    }
}

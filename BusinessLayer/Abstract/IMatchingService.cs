using EntityLayer.Entities;
using Shared.Dtos.PropertyListing;
using Shared.Dtos.PropertyRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IMatchingService
    {
        public Task<List<(PropertyRequestCardDto request, PropertyListingCardDto listing)>> FindMatches(string userId);
    }
}

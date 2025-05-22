using Shared.Dtos.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.PropertyListing.Detail
{
    public class CommercialUnitListingDetailDto : PropertyListingDetailDto, IPropertyListingDetailDto
    {
        public string SquareMetersize { get; set; }
        public string BlockNumber { get; set; }
        public string ParselNumber { get; set; }
        public string PricePerSquareMetersize { get; set; }
    }
}

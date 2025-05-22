using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.PropertyListing
{
    public class PropertyListingCardDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ClientNameSurname { get; set; }
        public string PropertyType { get; set; }
        public string PropertyStatus { get; set; }
        public string Address { get; set; }
        public string Price { get; set; }
        public string AddedDate { get; set; }
    }
}

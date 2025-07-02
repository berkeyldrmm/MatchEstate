using Shared.Dtos.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.PropertyListing.Detail
{
    public class PropertyListingDetailDto : IPropertyListingDetailDto
    {
        public string Id { get; set; }
        public string ClientNameSurname { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string ClientEmailAddress { get; set; }
        public string Title { get; set; }
        public int PropertyTypeId { get; set; }
        public string PropertyType { get; set; }
        public string PropertyStatus { get; set; }
        public string Address { get; set; }
        public string Price { get; set; }
        public string Commission { get; set; }
        public string Earning { get; set; }
        public bool DealStatus { get; set; }
        public string AddedDate { get; set; }
        public string Details { get; set; }
        public string ImageBase64 { get; set; }
    }
}

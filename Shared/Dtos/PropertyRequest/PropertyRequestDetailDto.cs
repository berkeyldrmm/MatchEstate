using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.PropertyRequest
{
    public class PropertyRequestDetailDto
    {
        public string Id { get; set; }
        public string ClientNameSurname { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string ClientEmailAddress { get; set; }
        public string Title { get; set; }
        public int PropertyTypeId { get; set; }
        public string PropertyType { get; set; }
        public string PropertyStatus { get; set; }
        public string City { get; set; }
        public string Districts { get; set; }
        public string NumberOfRooms { get; set; }
        public string MinPrice { get; set; }
        public string MaxPrice { get; set; }
        public string AddedDate { get; set; }
        public string Details { get; set; }
    }
}

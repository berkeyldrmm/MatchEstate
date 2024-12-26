
using EntityLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Property : IProperty
    {
        public string Id { get; set; }
        public string SquareMeterSize { get; set; }
        public decimal? PricePerSquareMeter { get; set; }
        public string ListingId { get; set; }
        public PropertyListing Listing { get; set; }
        public string BlockNumber { get; set; }
        public string ParselNumber { get; set; }
    }
}

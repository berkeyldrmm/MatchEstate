using EntityLayer.Entities;

namespace EntityLayer.Abstract
{
    public interface IProperty
    {
        public string Id { get; set; }
        public string SquareMeterSize { get; set; }
        public string BlockNumber { get; set; }
        public string ParselNumber { get; set; }
        public decimal? PricePerSquareMeter { get; set; }
        public string ListingId {  get; set; } 
        public PropertyListing Listing { get; set; }
    }
}

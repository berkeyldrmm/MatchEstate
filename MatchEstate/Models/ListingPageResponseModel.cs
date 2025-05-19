using Shared.Dtos.PropertyListing;

namespace MatchEstate.Models
{
    public class ListingPageResponseModel
    {
        public IEnumerable<ListingPageDTO> Listings { get; set; }
        public int TotalListingCount { get; set; }
    }
}

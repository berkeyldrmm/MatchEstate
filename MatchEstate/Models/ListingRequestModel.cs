namespace MatchEstate.Models
{
    public class ListingRequestModel
    {
        public string UserNameSurname { get; set; }
        public IEnumerable<AdminPageListingModel> Listings { get; set; }
        public IEnumerable<AdminPageRequestModel> Requests { get; set; }
    }
}

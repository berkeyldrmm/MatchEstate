namespace MatchEstate.Models
{
    public class ListingRequestModel
    {
        public string UserId { get; set; }
        public string UserNameSurname { get; set; }
        public IEnumerable<AdminPageListingModel> Listings { get; set; }
        public IEnumerable<AdminPageRequestModel> Requests { get; set; }
    }
}

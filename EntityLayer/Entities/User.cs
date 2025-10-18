using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Entities
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            Clients = new List<Client>();
            Listings = new List<PropertyListing>();
            Requests = new List<PropertyRequest>();
        }
        public string NameSurname { get; set; }
        public IEnumerable<Client> Clients { get; set; }
        public IEnumerable<PropertyListing> Listings { get; set; }
        public IEnumerable<PropertyRequest> Requests { get; set; }
        public string Tasks { get; set; } = "[]";
        public string IncomeExpenses { get; set; } = "[]";
        public DateTime? LastActiveDate { get; set; }
        public bool IsOnline { get; set; }
    }
}

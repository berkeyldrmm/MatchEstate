using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

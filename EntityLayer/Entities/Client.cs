using EntityLayer.Entities;
using System;
using System.Collections.Generic;

namespace EntityLayer.Entities;

public partial class Client
{
    public Client()
    {
        Listings = new List<PropertyListing>();
        Requests = new List<PropertyRequest>();
    }
    public string Id { get; set; }
    public string NameSurname { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public IEnumerable<PropertyListing> Listings { get; set; }
    public IEnumerable<PropertyRequest> Requests { get; set; }
}

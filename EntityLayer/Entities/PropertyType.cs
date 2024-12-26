using System;
using System.Collections.Generic;

namespace EntityLayer.Entities;

public partial class PropertyType
{
    public PropertyType()
    {
        Listings = new List<PropertyListing>();
        Requests = new List<PropertyRequest>();
    }
    public int Id { get; set; }
    public string PropertyName { get; set; }
    public IEnumerable<PropertyListing> Listings { get; set; }
    public IEnumerable<PropertyRequest> Requests { get; set; }
}

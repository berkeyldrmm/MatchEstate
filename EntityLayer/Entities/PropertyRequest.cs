
using EntityLayer.Entities;
using System;
using System.Collections.Generic;

namespace EntityLayer.Entities;

public partial class PropertyRequest
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string ClientId { get; set; }
    public Client Client { get; set; }
    public decimal MinimumPrice { get; set; }
    public decimal MaximumPrice { get; set; }
    public string? NumberOfRooms { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public int PropertyTypeId { get; set; }
    public PropertyType PropertyType { get; set; }
    public DateTime AddedDate { get; set; }
    public bool DealStatus { get; set; }
    public DateTime? DealDate { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public int PropertyStatusId { get; set; }
    public PropertyStatus PropertyStatus { get; set; }
    public string? Details { get; set; }
    public PropertyListing? PropertyListing { get; set; }
}

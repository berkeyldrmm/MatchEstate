
using EntityLayer.Abstract;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Entities;

public partial class PropertyListing
{
    public string Id { get; set; }
    public string Title { get; set; }
    public int PropertyTypeId { get; set; }
    public PropertyType PropertyType { get; set; }
    public string ClientId { get; set; }
    public Client Client { get; set; }
    public decimal Price { get; set; }
    public decimal Commission { get; set; }
    public decimal Earning { get; set; }
    public DateTime AddedDate { get; set; }
    public Land? Land { get; set; }
    public Farmland? Farmland { get; set; }
    public Apartment? Apartment { get; set; }
    public Shop? Shop { get; set; }
    public CommercialUnit? CommercialUnit { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public bool DealStatus { get; set; }
    public DateTime? DealDate { get; set; }
    public int PropertyStatusId { get; set; }
    public PropertyStatus PropertyStatus { get; set; }
    public string? Details { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string Neighbourhood { get; set; }
    [ForeignKey(nameof(PropertyRequest))]
    public string? PropertyRequestId { get; set; }
    public PropertyRequest? PropertyRequest { get; set; }
    public string? Images { get; set; }
    public string? ShareToken { get; set; }
    public DateTime? TokenExpirationDate { get; set; }
}

using Microsoft.AspNetCore.Http;

namespace Shared.Dtos.PropertyListing;

public class UpdateListingDto
{
    public string ListingId { get; set; }
    public string RadioForClient { get; set; }
    public string? ClientId { get; set; } = string.Empty;
    public string? ListingTitle { get; set; } = string.Empty;
    public string? ClientNameSurname { get; set; } = string.Empty;
    public string? ClientEmail { get; set; } = string.Empty;
    public string? ClientPhoneNumber { get; set; } = string.Empty;
    public int PropertyTypeId { get; set; }
    public int PropertyStatusId { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string Neighbourhood { get; set; }
    public string Price { get; set; }
    public string? Commission { get; set; } = string.Empty;
    public string? Details { get; set; } = string.Empty;
    public string? LocationUrl { get; set; } = string.Empty;
    public List<string> ExistingImages { get; set; }
    public List<string> DeletingImages { get; set; } = new List<string>();
    public List<IFormFile> NewImages { get; set; }
}

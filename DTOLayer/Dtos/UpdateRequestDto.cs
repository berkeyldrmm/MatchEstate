namespace DTOLayer.Dtos;

public class UpdateRequestDto
{
    public UpdateRequestDto()
    {
        NumberOfRooms = new List<string>();
        District = new List<string>();
    }
    public string RequestId { get; set; }
    public string RadioForClient { get; set; }
    public string RequestTitle { get; set; }
    public string? ClientId { get; set; }
    public string? ClientNameSurname { get; set; }
    public string? ClientEmail { get; set; }
    public string? ClientPhoneNumber { get; set; }
    public int PropertyTypeId { get; set; }
    public List<string> NumberOfRooms { get; set; }
    public int PropertyStatusId { get; set; }
    public string City { get; set; }
    public List<string> District { get; set; }
    public string? MinPrice { get; set; }
    public string? MaxPrice { get; set; }
    public string? Details { get; set; }
}

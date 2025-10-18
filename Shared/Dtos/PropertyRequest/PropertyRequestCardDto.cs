namespace Shared.Dtos.PropertyRequest
{
    public class PropertyRequestCardDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ClientNameSurname { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string PropertyType { get; set; }
        public string PropertyStatus { get; set; }
        public string City { get; set; }
        public string MinPrice { get; set; }
        public string MaxPrice { get; set; }
        public string AddedDate { get; set; }
    }
}

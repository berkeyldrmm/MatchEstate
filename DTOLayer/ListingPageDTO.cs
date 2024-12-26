namespace DTOLayer
{
    public class ListingPageDTO
    {
        public string Id { get; set; }
        public string ListingTitle { get; set; }
        public string PropertyType { get; set; }
        public string ClientNameSurname { get; set; }
        public string Price { get; set; }
        public string Commission { get; set; }
        public string Earning { get; set; }
        public bool Status { get; set; }
        public string IsForSaleOrRent { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Neighbourhood { get; set; }
    }
}

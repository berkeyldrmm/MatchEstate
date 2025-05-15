namespace DTOLayer.Dtos
{
    public class StatisticsModelDTO
    {
        public int? CountOfLandListings { get; set; }
        public int? CountOfApartmentListings { get; set; }
        public int? CountOfCommercialUnitListings { get; set; }
        public int? CountOfFarmlandListings { get; set; }
        public int? CountOfShopListings { get; set; }
        public int? CountOfLandRequests { get; set; }
        public int? CountOfApartmentRequests { get; set; }
        public int? CountOfCommercialUnitRequests { get; set; }
        public int? CountOfFarmlandRequests { get; set; }
        public int? CountOfShopRequests { get; set; }
        public List<PropertyStatusCountDto>? CountOfListingsPropertyStatuses { get; set; }
        public List<PropertyStatusCountDto>? CountOfRequestsPropertyStatuses { get; set; }
    }
}

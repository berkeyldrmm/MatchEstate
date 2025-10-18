namespace Shared.Dtos.PropertyRequest
{
    public class RequestGetByFiltersDTO
    {
        public string PropertyStatusId { get; set; }
        public int PropertyType { get; set; }
        public string Sort { get; set; }
        public string Status { get; set; }
        public string Search { get; set; }
        public string PageNumber { get; set; }
        public string PageSize { get; set; }
    }
}

using DTOLayer;

namespace MatchEstate.Models
{
    public class RequestPageResponseModel
    {
        public IEnumerable<RequestPageDTO> Requests { get; set; }
        public int TotalRequestCount { get; set; }
    }
}

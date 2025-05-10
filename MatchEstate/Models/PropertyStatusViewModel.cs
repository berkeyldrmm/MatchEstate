using EntityLayer.Entities;

namespace MatchEstate.Models
{
    public class PropertyStatusViewModel
    {
        public int? PropertyStatusId { get; set; }
        public IEnumerable<PropertyStatus> PropertyStatuses { get; set; }
    }
}

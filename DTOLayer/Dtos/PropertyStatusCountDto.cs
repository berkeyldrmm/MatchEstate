using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.Dtos
{
    public class PropertyStatusCountDto
    {
        public int PropertyStatusId { get; set; }
        public string PropertyStatus { get; set; }
        public int Count { get; set; }
        public string RgbColor { get; set; }
    }
}

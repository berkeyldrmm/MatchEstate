using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.PropertyRequest
{
    public class GetRequestsByPropertyTypeDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string PropertyType { get; set; }
    }
}

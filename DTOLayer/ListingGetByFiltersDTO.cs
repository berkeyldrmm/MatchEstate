using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer
{
    public class ListingGetByFiltersDTO
    {
        public string IsForSaleOrRent { get; set; }
        public int PropertyType { get; set; }
        public string Sort { get; set; }
        public string Search { get; set; }
        public string Status { get; set; }
        public string MinPrice { get; set; }
        public string MaxPrice { get; set; }
    }
}

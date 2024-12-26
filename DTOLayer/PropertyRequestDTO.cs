using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer
{
    public class PropertyRequestDTO
    {
        public string Id { get; set; }
        public string NameSurname { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string City { get; set; }
        public string MinPrice { get; set; }
        public string MaxPrice { get; set; }
        public string ForSaleOrRent { get; set; }
    }
}

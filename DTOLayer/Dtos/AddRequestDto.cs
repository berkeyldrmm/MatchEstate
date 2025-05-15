using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.Dtos
{
    public class AddRequestDto
    {
        public AddRequestDto()
        {
            NumberOfRooms = new List<string>();
            District = new List<string>();
        }
        public string RadioForClient { get; set; }
        public string RequestTitle { get; set; }
        public string? ClientId { get; set; }
        public string? ClientNameSurname { get; set; }
        public string? ClientEmail { get; set; }
        public string? ClientPhoneNumber { get; set; }
        public int PropertyTypeId { get; set; }
        public List<string> NumberOfRooms { get; set; }
        public int PropertyStatusId { get; set; }
        public string City { get; set; }
        public List<string> District { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? Details { get; set; }
    }
}
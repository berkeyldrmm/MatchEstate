﻿namespace Shared.Dtos.PropertyRequest
{
    public class RequestPageDTO
    {
        public string Id { get; set; }
        public string NameSurname { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string City { get; set; }
        public string MinPrice { get; set; }
        public string MaxPrice { get; set; }
        public string PropertyStatus { get; set; }
        public string AddedDate { get; set; }
        public bool Status { get; set; }
    }
}

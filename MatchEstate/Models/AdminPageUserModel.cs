﻿namespace MatchEstate.Models
{
    public class AdminPageUserModel
    {
        public string Id { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Activity { get; set; }
        public DateTime? LastActiveDate { get; set; }
    }
}

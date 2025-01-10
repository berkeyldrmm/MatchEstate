namespace MatchEstate.Models
{
    public class AdminUserViewModel
    {
        public IEnumerable<AdminPageUserModel> Admins { get; set; }
        public IEnumerable<AdminPageUserModel> Users { get; set; }
    }
}

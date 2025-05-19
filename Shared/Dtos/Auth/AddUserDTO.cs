namespace Shared.Dtos.Auth
{
    public class AddUserDTO
    {
        public string NameSurname { get; set; }
        public string Mail { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string PasswordCheck { get; set; }
    }
}

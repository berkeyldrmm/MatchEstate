namespace Shared.Dtos.Auth
{
    public class LoginDto
    {
        public string? UsernameOrMail { get; set; }
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}

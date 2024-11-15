namespace AuthServiceAPI.models
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string Username { get; set; }
        public string Role { get; set; }
    }
}

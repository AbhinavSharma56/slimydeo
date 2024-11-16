namespace AuthServiceAPI.models
{
    public class LoginResponse
    {
        public bool Success { get; set; } = false;
        public string Token { get; set; } = string.Empty;
        public string Username { get; set; }
        public string Role { get; set; }
    }
}

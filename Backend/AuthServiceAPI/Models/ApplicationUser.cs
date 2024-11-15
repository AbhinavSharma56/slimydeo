using Microsoft.AspNetCore.Identity;

namespace AuthServiceAPI.models
{
    public class ApplicationUser : IdentityUser
    {
        public string Role { get; set; } = string.Empty;
    }
}

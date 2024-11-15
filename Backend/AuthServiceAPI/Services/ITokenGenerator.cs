using AuthServiceAPI.models;

namespace AuthServiceAPI.Services
{
    public interface ITokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser);
    }
}

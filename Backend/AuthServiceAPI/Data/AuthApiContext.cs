using AuthServiceAPI.models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthServiceAPI.Data
{
    public class AuthApiContext:IdentityDbContext<ApplicationUser>
    {
        public AuthApiContext(DbContextOptions<AuthApiContext> options) : base(options) { }


    }
}

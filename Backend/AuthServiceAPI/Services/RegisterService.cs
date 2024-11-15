using AuthServiceAPI.Data;
using AuthServiceAPI.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthServiceAPI.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly AuthApiContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenGenerator _tokenGenerator;

        public RegisterService(AuthApiContext context, UserManager<ApplicationUser> userManager, ITokenGenerator tokenGenerator)
        {
            _context = context;
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<string> RegisterUser(RegisterRequestDto requestDto)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email = requestDto.Email,
                Role = requestDto.Role,
                UserName = requestDto.UserName,
            };
            var result = await _userManager.CreateAsync(user, requestDto.Password);
            if (result.Succeeded)
            {
                return "Register Succeeded";

            }
            return result.Errors.FirstOrDefault().Description;
        }


        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == loginRequest.UserName);
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequest.Password);



            if (isValid)
            {
                var token = _tokenGenerator.GenerateToken(user);

                return new LoginResponse()
                {
                    Token = token,
                    Role = user.Role,
                    Username = user.UserName
                };
            }
            return new LoginResponse();
        }
    }
}

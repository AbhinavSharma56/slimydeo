using AuthServiceAPI.models;
using AuthServiceAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthServiceAPI.Controllers
{
    [Route("api/AuthService")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly IRegisterService _registerService;

        public AuthApiController(IRegisterService registerService)
        {
            _registerService = registerService;
        }



        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterRequestDto register)
        {
            var result = await _registerService.RegisterUser(register);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest loginRequest)
        {
            var result = await _registerService.Login(loginRequest);
            return Ok(result);
        }
        
    }
}

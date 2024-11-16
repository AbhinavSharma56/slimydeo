using Microsoft.AspNetCore.Mvc;
using UserServiceAPI.Models;
using UserServiceAPI.Repository;

namespace UserServiceAPI.Controllers
{
    [Route("api/UserProfile")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        [HttpPost("AddUserProfile")]
        public async Task<IActionResult> AddUserProfile(UserProfile userProfile)
        {
            var response = await _userProfileRepository.AddUserProfileAsync(userProfile);
            return Ok(response);
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> GetUserProfileById(string userName)
        {
            var userProfile = await _userProfileRepository.GetUserProfileByUsernameAsync(userName);
            return userProfile != null
                ? Ok(new ApiResponse { Success = true, Message = "User profile retrieved successfully.", Data = userProfile })
                : NotFound(new ApiResponse { Success = false, Message = "User profile not found." });
        }

        [HttpGet("AllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userProfileRepository.GetAllUsersAsync();
            return Ok(new ApiResponse { Success = true, Message = "All users retrieved successfully.", Data = users });
        }

        [HttpPut("{userName}")]
        public async Task<IActionResult> UpdateUserProfile(string userName, UserProfile userProfile)
        {
            var response = await _userProfileRepository.UpdateUserProfileAsync(userName, userProfile);
            return Ok(response);
        }

        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteUserProfile(string userName)
        {
            var response = await _userProfileRepository.DeleteUserProfileAsync(userName);
            return Ok(response);
        }
    }
}
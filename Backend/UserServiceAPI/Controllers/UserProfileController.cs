using Microsoft.AspNetCore.Mvc;
using UserServiceAPI.Models;
using UserServiceAPI.Repository;

namespace UserServiceAPI.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("{userProfileId}")]
        public async Task<IActionResult> GetUserProfileById(int userProfileId)
        {
            var userProfile = await _userProfileRepository.GetUserProfileByIdAsync(userProfileId);
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

        [HttpPut("UpdateUserProfile")]
        public async Task<IActionResult> UpdateUserProfile(UserProfile userProfile)
        {
            var response = await _userProfileRepository.UpdateUserProfileAsync(userProfile);
            return Ok(response);
        }

        [HttpDelete("{userProfileId}")]
        public async Task<IActionResult> DeleteUserProfile(int userProfileId)
        {
            var response = await _userProfileRepository.DeleteUserProfileAsync(userProfileId);
            return Ok(response);
        }
    }
}
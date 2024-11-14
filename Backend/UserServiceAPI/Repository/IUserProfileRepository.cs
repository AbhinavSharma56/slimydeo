using UserServiceAPI.Models;

namespace UserServiceAPI.Repository
{
    public interface IUserProfileRepository
    {
        Task<ApiResponse> AddUserProfileAsync(UserProfile userProfile);
        Task<UserProfile?> GetUserProfileByIdAsync(int userProfileId);
        Task<IEnumerable<UserProfile>> GetAllUsersAsync();
        Task<ApiResponse> UpdateUserProfileAsync(UserProfile userProfile);
        Task<ApiResponse> DeleteUserProfileAsync(int userProfileId);
    }
}
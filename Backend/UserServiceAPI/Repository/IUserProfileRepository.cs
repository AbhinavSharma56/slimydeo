using UserServiceAPI.Models;

namespace UserServiceAPI.Repository
{
    public interface IUserProfileRepository
    {
        Task<ApiResponse> AddUserProfileAsync(UserProfile userProfile);
        Task<UserProfile?> GetUserProfileByUsernameAsync(string userName);
        Task<IEnumerable<UserProfile>> GetAllUsersAsync();
        Task<ApiResponse> UpdateUserProfileAsync(string userName, UserProfile userProfile);
        Task<ApiResponse> DeleteUserProfileAsync(string userName);
    }
}
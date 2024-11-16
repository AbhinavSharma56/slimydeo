using Microsoft.EntityFrameworkCore;
using UserServiceAPI.Data;
using UserServiceAPI.Models;

namespace UserServiceAPI.Repository
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly UserDbContext _context;

        public UserProfileRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse> AddUserProfileAsync(UserProfile userProfile)
        {
            _context.UserProfiles.Add(userProfile);
            await _context.SaveChangesAsync();
            return new ApiResponse { Success = true, Message = "User profile created successfully.", Data = userProfile };
        }

        public async Task<UserProfile?> GetUserProfileByUsernameAsync(string userName)
        {
            return await _context.UserProfiles.FirstOrDefaultAsync(x => x.Username==userName);
        }

        public async Task<IEnumerable<UserProfile>> GetAllUsersAsync()
        {
            return await _context.UserProfiles.ToListAsync();
        }

        public async Task<ApiResponse> UpdateUserProfileAsync(string userName, UserProfile userProfile)
        {
            var existingUserProfile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.Username == userName);
            if (existingUserProfile == null)
            {
                return new ApiResponse { Success = false, Message = "User profile not found." };
            }

            existingUserProfile.FullName = userProfile.FullName;
            existingUserProfile.DOB = userProfile.DOB;
            existingUserProfile.Email = userProfile.Email;
            existingUserProfile.MobileNumber = userProfile.MobileNumber;
            existingUserProfile.Gender = userProfile.Gender;
            existingUserProfile.Address = userProfile.Address;
            existingUserProfile.ProfilePicture = userProfile.ProfilePicture;
            existingUserProfile.Weight = userProfile.Weight;
            existingUserProfile.Height = userProfile.Height;

            await _context.SaveChangesAsync();
            return new ApiResponse { Success = true, Message = "User profile updated successfully.", Data = userProfile };
        }

        public async Task<ApiResponse> DeleteUserProfileAsync(string userName)
        {
            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.Username == userName);
            if (userProfile == null)
            {
                return new ApiResponse { Success = false, Message = "User profile not found." };
            }

            _context.UserProfiles.Remove(userProfile);
            await _context.SaveChangesAsync();
            return new ApiResponse { Success = true, Message = "User profile deleted successfully." };
        }
    }
}
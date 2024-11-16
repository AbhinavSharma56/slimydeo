using DietServiceAPI.Data;
using DietServiceAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DietServiceAPI.Repository
{
    public class FoodDetailsRepository : IFoodDetailsRepository
    {
        private readonly DietDbContext _context;

        public FoodDetailsRepository(DietDbContext context)
        {
            _context = context;
        }

        public async Task AddFoodDetailsAsync(FoodDetails foodDetails)
        {
            try
            {
                await _context.FoodDetails.AddAsync(foodDetails);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (implement proper logging here)
                Console.Error.WriteLine($"Error in AddFoodDetailsAsync: {ex.Message}");
                throw new Exception("Database operation failed while adding food details.");
            }
        }

        public async Task<bool> DeleteFoodDetailsAsync(int foodId)
        {
            var response = await _context.FoodDetails.FirstOrDefaultAsync(x  => x.FoodId == foodId);
            if (response != null)
            {
                _context.FoodDetails.Remove(response);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<ApiResponse<FoodDetails>> GetFoodDetailsByIdAsync(int foodId)
        {
            var response = await _context.FoodDetails.FirstOrDefaultAsync(x => x.FoodId == foodId);
            if (response != null)
            {
                return new ApiResponse<FoodDetails>(true, "Food details retrieved successfully.", response);
            }
            return new ApiResponse<FoodDetails>(false, "Food details not found.");
        }

        public async Task<List<FoodDetails>> GetFoodDetailsByFoodIds(List<int> foodIds)
        {
            try
            {
                var foodDetails = await _context.FoodDetails
                    .Where(fd => foodIds.Contains(fd.FoodId))
                    .ToListAsync();

                return foodDetails;
            }
            catch (Exception ex)
            {
                // Log the exception (use proper logging)
                Console.Error.WriteLine($"Error in GetFoodDetailsByFoodIds: {ex.Message}");
                throw new Exception("Failed to retrieve food details for the provided food IDs.");
            }
        }

    }
}
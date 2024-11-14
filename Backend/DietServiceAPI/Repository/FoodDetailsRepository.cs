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
            await _context.FoodDetails.AddAsync(foodDetails);
            await _context.SaveChangesAsync();
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
    }
}
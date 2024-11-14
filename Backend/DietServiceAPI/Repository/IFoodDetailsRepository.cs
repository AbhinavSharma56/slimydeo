using DietServiceAPI.Models;

namespace DietServiceAPI.Repository
{
    public interface IFoodDetailsRepository
    {
        Task AddFoodDetailsAsync(FoodDetails foodDetails);
        Task<bool> DeleteFoodDetailsAsync(int foodId);
        Task<ApiResponse<FoodDetails>> GetFoodDetailsByIdAsync(int foodId);
    }
}

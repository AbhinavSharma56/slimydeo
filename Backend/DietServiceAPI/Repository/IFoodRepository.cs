using DietServiceAPI.Models;

namespace DietServiceAPI.Repository
{
    public interface IFoodRepository
    {
        Task<ApiResponse<IEnumerable<Food>>> GetAllFoodsAsync();
        Task<ApiResponse<Food>> GetFoodByIdAsync(int foodId);
        Task<ApiResponse<Food>> AddFoodAsync(Food food);
        Task<ApiResponse<Food>> UpdateFoodAsync(Food food);
        Task<ApiResponse<bool>> DeleteFoodAsync(int foodId);
        Task<ApiResponse<IEnumerable<Food>>> GetFoodsByMealIdAsync(int mealId);
    }
}

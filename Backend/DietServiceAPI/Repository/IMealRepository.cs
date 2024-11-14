using DietServiceAPI.Models;

namespace DietServiceAPI.Repository
{
    public interface IMealRepository
    {
        Task<ApiResponse<IEnumerable<Meal>>> GetAllMealsAsync();
        Task<ApiResponse<Meal>> GetMealByIdAsync(int mealId);
        Task<ApiResponse<Meal>> AddMealAsync(Meal meal);
        Task<ApiResponse<Meal>> UpdateMealAsync(Meal meal);
        Task<ApiResponse<bool>> DeleteMealAsync(int mealId);
    }
}
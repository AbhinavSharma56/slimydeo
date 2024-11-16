using DietServiceAPI.Models;

namespace DietServiceAPI.Service
{
    public interface IFoodDetailsService
    {
        Task<bool> AddFoodDetails(int foodId, string foodName, int quantity, string unit);
        Task<ApiResponse<bool>> AddMultipleFoodDetails(List<FoodDetailsRequestDTO> foodDetailsRequests);
    }
}

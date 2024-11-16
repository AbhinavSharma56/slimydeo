using DietServiceAPI.Data;
using DietServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DietServiceAPI.Repository
{
    public class FoodRepository : IFoodRepository
    {
        private readonly DietDbContext _context;

        public FoodRepository(DietDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<IEnumerable<Food>>> GetAllFoodsAsync()
        {
            var foods = await _context.Foods.ToListAsync();
            if (foods == null || foods.Count == 0)
            {
                return new ApiResponse<IEnumerable<Food>>(false, "No foods found");
            }
            return new ApiResponse<IEnumerable<Food>>(true, "Foods retrieved successfully", foods);
        }

        public async Task<ApiResponse<Food>> GetFoodByIdAsync(int foodId)
        {
            var food = await _context.Foods.FindAsync(foodId);
            if (food == null)
            {
                return new ApiResponse<Food>(false, "Food not found");
            }
            return new ApiResponse<Food>(true, "Food retrieved successfully", food);
        }

        public async Task<ApiResponse<Food>> AddFoodAsync(Food food)
        {
            _context.Foods.Add(food);
            await _context.SaveChangesAsync();
            return new ApiResponse<Food>(true, "Food added successfully", food);
        }

        public async Task<ApiResponse<Food>> UpdateFoodAsync(Food food)
        {
            var existingFood = await _context.Foods.FindAsync(food.FoodId);
            if (existingFood == null)
            {
                return new ApiResponse<Food>(false, "Food not found");
            }

            _context.Foods.Update(food);
            await _context.SaveChangesAsync();
            return new ApiResponse<Food>(true, "Food updated successfully", food);
        }

        public async Task<ApiResponse<bool>> DeleteFoodAsync(int foodId)
        {
            var food = await _context.Foods.FindAsync(foodId);
            if (food == null)
            {
                return new ApiResponse<bool>(false, "Food not found");
            }

            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();
            return new ApiResponse<bool>(true, "Food deleted successfully", true);
        }

        public async Task<ApiResponse<IEnumerable<Food>>> GetFoodsByMealIdAsync(int mealId)
        {
            try
            {
                var foods = await _context.Foods
                                          .Where(f => f.MealId == mealId)
                                          .ToListAsync();

                if (foods.Any())
                {
                    return new ApiResponse<IEnumerable<Food>>(true, "Foods retrieved successfully", foods);
                }

                return new ApiResponse<IEnumerable<Food>>(false, "No foods found for the given MealId");
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<Food>>(false, $"An error occurred: {ex.Message}");
            }
        }

        public async Task<ApiResponse<List<Food>>> AddMultipleFoodsAsync(List<Food> foods)
        {
            try
            {
                foreach (var food in foods)
                {
                    _context.Foods.Add(food); // Add each food item to the context
                }

                await _context.SaveChangesAsync(); // Save all changes at once

                return new ApiResponse<List<Food>>(true, "Foods added successfully", foods);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<Food>>(false, $"An error occurred: {ex.Message}");
            }
        }
        public async Task<List<Food>> GetFoodsByMealIds(List<int> mealIds)
        {
            try
            {
                var foods = await _context.Foods
                    .Where(f => mealIds.Contains(f.MealId))
                    .ToListAsync();

                return foods;
            }
            catch (Exception ex)
            {
                // Log the exception (use proper logging)
                Console.Error.WriteLine($"Error in GetFoodsByMealIds: {ex.Message}");
                throw new Exception("Failed to retrieve food items for the provided meal IDs.");
            }
        }

    }
}

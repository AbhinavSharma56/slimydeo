using DietServiceAPI.Data;
using DietServiceAPI.Models;
using DietServiceAPI.Service;
using Microsoft.EntityFrameworkCore;

namespace DietServiceAPI.Repository
{
    public class FoodRepository : IFoodRepository
    {
        private readonly DietDbContext _context;
        private readonly IFoodDetailsService _foodDetailsService;
        private readonly IFoodDetailsRepository _foodDetailsRepository;

        public FoodRepository(DietDbContext context, IFoodDetailsService foodDetailsService, IFoodDetailsRepository foodDetailsRepository)
        {
            _context = context;
            _foodDetailsService = foodDetailsService;
            _foodDetailsRepository = foodDetailsRepository;
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
            try
            {
                // Add the food to the database
                _context.Foods.Add(food);
                await _context.SaveChangesAsync();

                int foodId = food.FoodId;
                string foodName = food.FoodName;
                int quantity = food.Quantity;
                string unit = food.Unit;

                // After saving the food, add the food details using the Nutritionix API
                bool isFoodDetailsAdded = await _foodDetailsService.AddFoodDetails(foodId, foodName, quantity, unit);

                if (isFoodDetailsAdded)
                {
                    return new ApiResponse<Food>(true, "Food and Food details added successfully", food);
                }
                else
                {
                    return new ApiResponse<Food>(false, "Food added, but failed to retrieve food details.", food);
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<Food>(false, $"An error occurred: {ex.Message}", null);
            }
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

        public async Task<ApiResponse<List<int>>> AddMultipleFoodsAsync(List<Food> foods)
        {
            try
            {
                var addedFoodIds = new List<int>();

                foreach (var food in foods)
                {
                    _context.Foods.Add(food); // Add each food item to the context
                }

                await _context.SaveChangesAsync(); // Save all changes at once

                // After saving foods, we can retrieve the FoodId for each added food and add their details.
                foreach (var food in foods)
                {
                    bool isFoodDetailsAdded = await _foodDetailsService.AddFoodDetails(food.FoodId, food.FoodName, food.Quantity, food.Unit);
                    if (!isFoodDetailsAdded)
                    {
                        // Logging the failure to add food details for a particular food item.
                        Console.WriteLine($"Failed to add details for FoodId: {food.FoodId}");
                    }
                }

                // Retrieve IDs of the newly added food items
                addedFoodIds = foods.Select(f => f.FoodId).ToList();

                return new ApiResponse<List<int>>(true, "Foods and their details added successfully", addedFoodIds);
            }
            catch (Exception ex)
            {
                // Log the exception and return a failure response
                return new ApiResponse<List<int>>(false, $"An error occurred: {ex.Message}");
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

        public async Task<ApiResponse<List<Food>>> UpdateMultipleFoodsAsync(List<Food> foods)
        {
            try
            {
                var updatedFoods = new List<Food>();
                var errors = new List<string>();

                foreach (var food in foods)
                {
                    var existingFood = await _context.Foods.FindAsync(food.FoodId);
                    if (existingFood == null)
                    {
                        // Return a response indicating that one of the foods was not found
                        errors.Add($"Food with ID {food.FoodId} not found.");
                        continue;
                    }

                    // Update the food properties
                    existingFood.FoodName = food.FoodName;
                    existingFood.Quantity = food.Quantity;
                    existingFood.Unit = food.Unit;

                    _context.Foods.Update(existingFood); // Mark the entity as updated
                    updatedFoods.Add(existingFood);

                    // Delete the existing food details
                    var foodDetailsDeleted = await _foodDetailsRepository.DeleteFoodDetailsAsync(food.FoodId);
                    if (!foodDetailsDeleted)
                    {
                        errors.Add($"Failed to delete food details for FoodId: {food.FoodId}");
                        continue;
                    }

                    // Re-add the updated food details
                    bool isFoodDetailsUpdated = await _foodDetailsService.AddFoodDetails(food.FoodId, food.FoodName, food.Quantity, food.Unit);
                    if (!isFoodDetailsUpdated)
                    {
                        // Log or handle the failure to update food details for a particular food item
                        errors.Add($"Failed to update details for FoodId: {food.FoodId}");
                    }
                }

                // Save all changes to the database
                await _context.SaveChangesAsync();

                // Check if there were any errors
                if (errors.Any())
                {
                    return new ApiResponse<List<Food>>(false, string.Join("; ", errors));
                }

                return new ApiResponse<List<Food>>(true, "Foods and their details updated successfully", updatedFoods);
            }
            catch (Exception ex)
            {
                // Log the exception and return a failure response
                return new ApiResponse<List<Food>>(false, $"An error occurred: {ex.Message}", null);
            }
        }



    }
}

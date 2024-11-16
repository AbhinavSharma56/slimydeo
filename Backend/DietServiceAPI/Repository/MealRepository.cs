using DietServiceAPI.Data;
using DietServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DietServiceAPI.Repository
{
    public class MealRepository : IMealRepository
    {
        private readonly DietDbContext _context;

        public MealRepository(DietDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<IEnumerable<Meal>>> GetAllMealsAsync()
        {
            var meals = await _context.Meals.ToListAsync();
            if (meals == null || meals.Count == 0)
            {
                return new ApiResponse<IEnumerable<Meal>>(false, "No meals found");
            }
            return new ApiResponse<IEnumerable<Meal>>(true, "Meals retrieved successfully", meals);
        }

        public async Task<ApiResponse<Meal>> GetMealByIdAsync(int mealId)
        {
            var meal = await _context.Meals.FindAsync(mealId);
            if (meal == null)
            {
                return new ApiResponse<Meal>(false, "Meal not found");
            }
            return new ApiResponse<Meal>(true, "Meal retrieved successfully", meal);
        }

        public async Task<ApiResponse<Meal>> AddMealAsync(Meal meal)
        {
            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();
            return new ApiResponse<Meal>(true, "Meal added successfully", meal);
        }

        public async Task<ApiResponse<Meal>> UpdateMealAsync(Meal meal)
        {
            var existingMeal = await _context.Meals.FindAsync(meal.MealId);
            if (existingMeal == null)
            {
                return new ApiResponse<Meal>(false, "Meal not found");
            }

            existingMeal.Username = meal.Username;
            existingMeal.MealType = meal.MealType;
            existingMeal.ConsumptionDate = meal.ConsumptionDate;
            await _context.SaveChangesAsync();
            return new ApiResponse<Meal>(true, "Meal updated successfully", meal);
        }

        public async Task<ApiResponse<bool>> DeleteMealAsync(int mealId)
        {
            var meal = await _context.Meals.FindAsync(mealId);
            if (meal == null)
            {
                return new ApiResponse<bool>(false, "Meal not found");
            }

            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();
            return new ApiResponse<bool>(true, "Meal deleted successfully", true);
        }

        public async Task<ApiResponse<IEnumerable<Meal>>> GetMealsByUserAsync(string userName)
        {
            // Filter meals by the given username
            var meals = await _context.Meals
                .Where(m => m.Username == userName)
                .ToListAsync();

            // Check if any meals were found
            if (meals == null || meals.Count == 0)
            {
                return new ApiResponse<IEnumerable<Meal>>(false, "No meals found for the given username");
            }

            // Return the meals
            return new ApiResponse<IEnumerable<Meal>>(true, "Meals retrieved successfully", meals);
        }

        public async Task<List<Meal>> GetMealsForUserByDate(string username, DateTime date)
        {
            try
            {
                var meals = await _context.Meals
                    .Where(m => m.Username == username && m.ConsumptionDate.Date == date.Date)
                    .ToListAsync();

                return meals;
            }
            catch (Exception ex)
            {
                // Log the exception (use proper logging)
                Console.Error.WriteLine($"Error in GetMealsForUserByDate: {ex.Message}");
                throw new Exception("Failed to retrieve meals for the specified user and date.");
            }
        }

    }
}
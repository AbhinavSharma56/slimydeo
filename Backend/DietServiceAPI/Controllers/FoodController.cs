using DietServiceAPI.Models;
using DietServiceAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DietServiceAPI.Controllers
{
    [Route("api/Food")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodRepository _foodRepository;

        public FoodController(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        // GET: api/food
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Food>>>> GetFoods()
        {
            var response = await _foodRepository.GetAllFoodsAsync();
            if (!response.Success)
            {
                return NotFound(response); // Return failure response
            }
            return Ok(response); // Return success response
        }

        // GET: api/food/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Food>>> GetFood(int id)
        {
            var response = await _foodRepository.GetFoodByIdAsync(id);
            if (!response.Success)
            {
                return NotFound(response); // Return failure if food not found
            }
            return Ok(response); // Return success response
        }

        // POST: api/food
        [HttpPost("add")]
        public async Task<ActionResult<ApiResponse<Food>>> CreateFood(Food food)
        {
            var response = await _foodRepository.AddFoodAsync(food);
            return CreatedAtAction(nameof(GetFood), new { id = response.Data.FoodId }, response); // Return the success response
        }

        // PUT: api/food/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Food>>> UpdateFood(int id, Food food)
        {
            if (id != food.FoodId)
            {
                return BadRequest(new ApiResponse<Food>(false, "Food ID mismatch"));
            }

            var response = await _foodRepository.UpdateFoodAsync(food);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response); // Return success response
        }

        // DELETE: api/food/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteFood(int id)
        {
            var response = await _foodRepository.DeleteFoodAsync(id);
            if (!response.Success)
            {
                return NotFound(response); // Return failure response if food not found
            }
            return Ok(response); // Return success response
        }

        [HttpGet("meal/{mealId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Food>>>> GetFoodsByMealId(int mealId)
        {
            var response = await _foodRepository.GetFoodsByMealIdAsync(mealId);
            if (!response.Success)
            {
                return NotFound(response); // Return failure if no foods found for the given MealId
            }
            return Ok(response); // Return success with the foods list
        }


        // POST: api/food/bulk-add
        [HttpPost("bulk-add")]
        public async Task<ActionResult<ApiResponse<List<Food>>>> AddMultipleFoods(List<Food> foods)
        {
            if (foods == null || !foods.Any())
            {
                return BadRequest(new ApiResponse<List<Food>>(false, "Food list cannot be empty."));
            }

            var response = await _foodRepository.AddMultipleFoodsAsync(foods);

            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode(500, response); // Return a 500 Internal Server Error if something went wrong
        }

        [HttpPost("foods-by-meal-ids")]
        public async Task<IActionResult> GetFoodsByMealIds([FromBody] List<int> mealIds)
        {
            try
            {
                if (mealIds == null || !mealIds.Any())
                {
                    return BadRequest(new ApiResponse<List<Food>>(false, "Meal IDs cannot be null or empty."));
                }

                var foods = await _foodRepository.GetFoodsByMealIds(mealIds);

                if (foods == null || !foods.Any())
                {
                    return NotFound(new ApiResponse<List<Food>>(false, "No food items found for the provided meal IDs."));
                }

                return Ok(new ApiResponse<List<Food>>(true, "Food items retrieved successfully.", foods));
            }
            catch (Exception ex)
            {
                // Log the exception (use proper logging)
                Console.Error.WriteLine($"Error in GetFoodsByMealIds: {ex.Message}");
                return StatusCode(500, new ApiResponse<List<Food>>(false, "An internal server error occurred."));
            }
        }

    }
}

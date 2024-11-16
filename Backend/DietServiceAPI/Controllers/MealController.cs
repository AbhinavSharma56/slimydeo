using DietServiceAPI.Models;
using DietServiceAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DietServiceAPI.Controllers
{
    [Route("api/Meal")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IMealRepository _mealRepository;

        public MealController(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        // GET: api/meal
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Meal>>>> GetMeals()
        {
            var response = await _mealRepository.GetAllMealsAsync();
            if (!response.Success)
            {
                return NotFound(response); // Return a failure response
            }
            return Ok(response); // Return success response
        }

        // GET: api/meal/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Meal>>> GetMeal(int id)
        {
            var response = await _mealRepository.GetMealByIdAsync(id);
            if (!response.Success)
            {
                return NotFound(response); // Return failure if meal not found
            }
            return Ok(response); // Return success response
        }

        // GET: api/meal/user/{userName}
        [HttpGet("user/{userName}")]
        public async Task<ActionResult<ApiResponse<Meal>>> GetMealByUser(string userName)
        {
            var response = await _mealRepository.GetMealsByUserAsync(userName);
            if (!response.Success)
            {
                return NotFound(response); // Return failure if meal not found
            }
            return Ok(response); // Return success response
        }

        // POST: api/meal
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Meal>>> CreateMeal(Meal meal)
        {
            var response = await _mealRepository.AddMealAsync(meal);
            return CreatedAtAction(nameof(GetMeal), new { id = response.Data.MealId }, response); // Return the success response
        }

        // PUT: api/meal/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Meal>>> UpdateMeal(int id, Meal meal)
        {
            if (id != meal.MealId)
            {
                return BadRequest(new ApiResponse<Meal>(false, "Meal ID mismatch"));
            }

            var response = await _mealRepository.UpdateMealAsync(meal);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response); // Return success response
        }

        // DELETE: api/meal/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteMeal(int id)
        {
            var response = await _mealRepository.DeleteMealAsync(id);
            if (!response.Success)
            {
                return NotFound(response); // Return failure response if meal not found
            }
            return Ok(response); // Return success response
        }

        [HttpGet("user-meals")]
        public async Task<IActionResult> GetMealsForUserByDate(string username, DateTime date)
        {
            try
            {
                var meals = await _mealRepository.GetMealsForUserByDate(username, date);
                if (meals == null || !meals.Any())
                {
                    return NotFound(new ApiResponse<List<Meal>>(false, "No meals found for the specified user and date."));
                }
                return Ok(new ApiResponse<List<Meal>>(true, "Meals retrieved successfully.", meals));
            }
            catch (Exception ex)
            {
                // Log the exception (use proper logging)
                Console.Error.WriteLine($"Error in GetMealsForUserByDate: {ex.Message}");
                return StatusCode(500, new ApiResponse<List<Meal>>(false, "An internal server error occurred."));
            }
        }

    }
}
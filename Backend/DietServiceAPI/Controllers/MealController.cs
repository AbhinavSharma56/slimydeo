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
    }
}
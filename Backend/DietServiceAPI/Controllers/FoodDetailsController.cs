using DietServiceAPI.Models;
using DietServiceAPI.Repository;
using DietServiceAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace DietServiceAPI.Controllers
{
    [Route("api/FoodDetails")]
    [ApiController]
    public class FoodDetailsController : ControllerBase
    { 
        private readonly IFoodDetailsRepository _foodDetailsRepository;
        private readonly IFoodDetailsService _foodDetailsService;

        public FoodDetailsController(IFoodDetailsService foodDetailsService, IFoodDetailsRepository foodDetailsRepository)
        {
            _foodDetailsService = foodDetailsService;
            _foodDetailsRepository = foodDetailsRepository;
        }

        [HttpPost("add-food-details")]
        public async Task<IActionResult> AddFoodDetails(int foodId, string foodName, int quantity, string unit)
        {
            var result = await _foodDetailsService.AddFoodDetails(foodId, foodName, quantity, unit);
            if (result)
            {
                return Ok(new ApiResponse<bool>(true, "Food details added successfully.", true));
            }
            return BadRequest(new ApiResponse<bool>(false, "Failed to add food details."));
        }

        [HttpDelete("{foodId}")]
        public async Task<IActionResult> DeleteFoodDetails(int foodId)
        {
            var result = await _foodDetailsRepository.DeleteFoodDetailsAsync(foodId);
            if (result)
            {
                return Ok(new ApiResponse<bool>(true, "Food details deleted successfully.", true));
            }
            return BadRequest(new ApiResponse<bool>(false, "Failed to delete food details."));
        }

        [HttpGet("{foodId}")]
        public async Task<IActionResult> GetFoodDetailsById(int foodId)
        {
            var result = await _foodDetailsRepository.GetFoodDetailsByIdAsync(foodId);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

    }
}
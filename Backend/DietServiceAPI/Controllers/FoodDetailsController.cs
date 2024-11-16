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

        [HttpPost("add-food-details-bulk")]
        public async Task<IActionResult> AddMultipleFoodDetails([FromBody] List<FoodDetailsRequestDTO> foodDetailsRequests)
        {
            try
            {
                var result = await _foodDetailsService.AddMultipleFoodDetails(foodDetailsRequests);

                if (result.Success)
                {
                    return Ok(new ApiResponse<bool>(true, "Food details added successfully.", true));
                }
                return BadRequest(new ApiResponse<bool>(false, result.Message));
            }
            catch (Exception ex)
            {
                // Log the exception (implement proper logging here)
                Console.Error.WriteLine($"Error in AddMultipleFoodDetails: {ex.Message}");
                return StatusCode(500, new ApiResponse<bool>(false, "An internal server error occurred."));
            }
        }

        [HttpPost("food-details-by-food-ids")]
        public async Task<IActionResult> GetFoodDetailsByFoodIds([FromBody] List<int> foodIds)
        {
            try
            {
                if (foodIds == null || !foodIds.Any())
                {
                    return BadRequest(new ApiResponse<List<FoodDetails>>(false, "Food IDs cannot be null or empty."));
                }

                var foodDetails = await _foodDetailsRepository.GetFoodDetailsByFoodIds(foodIds);

                if (foodDetails == null || !foodDetails.Any())
                {
                    return NotFound(new ApiResponse<List<FoodDetails>>(false, "No food details found for the provided food IDs."));
                }

                return Ok(new ApiResponse<List<FoodDetails>>(true, "Food details retrieved successfully.", foodDetails));
            }
            catch (Exception ex)
            {
                // Log the exception (use proper logging)
                Console.Error.WriteLine($"Error in GetFoodDetailsByFoodIds: {ex.Message}");
                return StatusCode(500, new ApiResponse<List<FoodDetails>>(false, "An internal server error occurred."));
            }
        }


    }
}
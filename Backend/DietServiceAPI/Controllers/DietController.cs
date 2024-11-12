using DietServiceAPI.Models;
using DietServiceAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DietServiceAPI.Controllers
{
    [Route("api/Diet")]
    [ApiController]
    public class DietController : ControllerBase
    {
        private readonly IDietRepository _dietRepository;

        public DietController(IDietRepository dietRepository)
        {
            _dietRepository = dietRepository;
        }

        // GET: api/Diet
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var diets = await _dietRepository.GetDietsAsync();
            return Ok(diets);
        }

        // GET: api/Diet/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var diet = await _dietRepository.GetDietByIdAsync(id);
            if (diet == null)
            {
                return NotFound("Diet not found.");
            }
            return Ok(diet);
        }

        // POST: api/Diet
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Diet diet)
        {
            if (diet == null)
            {
                return BadRequest("Diet is null.");
            }

            await _dietRepository.InsertDietAsync(diet);
            return CreatedAtAction(nameof(Get), new { id = diet.DietId }, diet);
        }

        // PUT: api/Diet/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Diet diet)
        {
            if (diet == null || diet.DietId != id)
            {
                return BadRequest("Diet ID mismatch.");
            }

            var existingDiet = await _dietRepository.GetDietByIdAsync(id);
            if (existingDiet == null)
            {
                return NotFound("Diet not found.");
            }

            // Update the existing diet's fields
            existingDiet.Username = diet.Username;
            existingDiet.FoodItem = diet.FoodItem;
            existingDiet.Quantity = diet.Quantity;
            existingDiet.Calories = diet.Calories;
            existingDiet.MealType = diet.MealType;
            existingDiet.ConsumptionDate = diet.ConsumptionDate;

            await _dietRepository.UpdateDietAsync(existingDiet);
            return Ok("Diet updated successfully.");
        }

        // DELETE: api/Diet/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var diet = await _dietRepository.GetDietByIdAsync(id);
            if (diet == null)
            {
                return NotFound("Diet not found.");
            }

            await _dietRepository.DeleteDietAsync(id);
            return Ok("Diet deleted successfully.");
        }
    }
}

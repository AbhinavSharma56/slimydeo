using ExerciseServiceAPI.Models;
using ExerciseServiceAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExerciseServiceAPI.Controllers
{
    [Route("api/ExerciseType")]
    [ApiController]
    public class ExerciseTypeController : ControllerBase
    {
        private readonly IExerciseTypeRepository _exerciseTypeRepository;

        public ExerciseTypeController(IExerciseTypeRepository exerciseTypeRepository)
        {
            _exerciseTypeRepository = exerciseTypeRepository;
        }

        // GET: api/exercisetypes
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var exerciseTypes = await _exerciseTypeRepository.GetExerciseTypesAsync();
            return Ok(exerciseTypes);
        }

        // GET: api/exercisetype/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var exerciseType = await _exerciseTypeRepository.GetExerciseTypeByIdAsync(id);
            if (exerciseType == null)
            {
                return NotFound("Exercise Type not found.");
            }
            return Ok(exerciseType);
        }

        // POST: api/exercisetype
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ExerciseType exerciseType)
        {
            if (exerciseType == null)
            {
                return BadRequest("Exercise Type is null.");
            }

            await _exerciseTypeRepository.InsertExerciseTypeAsync(exerciseType);
            return CreatedAtAction(nameof(Get), new { id = exerciseType.ExerciseTypeId }, exerciseType);
        }

        // PUT: api/exercisetype/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ExerciseType exerciseType)
        {
            if (exerciseType == null || exerciseType.ExerciseTypeId != id)
            {
                return BadRequest("Exercise Type ID mismatch.");
            }

            var existingExerciseType = await _exerciseTypeRepository.GetExerciseTypeByIdAsync(id);
            if (existingExerciseType == null)
            {
                return NotFound("Exercise Type not found.");
            }

            // Update fields
            existingExerciseType.ExerciseName = exerciseType.ExerciseName;
            existingExerciseType.Description = exerciseType.Description;
            existingExerciseType.CreatedBy = exerciseType.CreatedBy;
            existingExerciseType.CreatedAt = exerciseType.CreatedAt;

            await _exerciseTypeRepository.UpdateExerciseTypeAsync(existingExerciseType);
            return Ok("Exercise Type updated successfully.");
        }

        // DELETE: api/exercisetype/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exerciseType = await _exerciseTypeRepository.GetExerciseTypeByIdAsync(id);
            if (exerciseType == null)
            {
                return NotFound("Exercise Type not found.");
            }

            await _exerciseTypeRepository.DeleteExerciseTypeAsync(id);
            return Ok("Exercise Type deleted successfully.");
        }

        // GET: api/exercisetype/name/{exerciseTypeId}
        [HttpGet("name/{exerciseTypeId}")]
        public async Task<IActionResult> GetExerciseName(int exerciseTypeId)
        {
            var exerciseName = await _exerciseTypeRepository.GetExerciseNameByIdAsync(exerciseTypeId);
            if (exerciseName == null)
                return NotFound("Exercise type not found.");

            return Ok(new { ExerciseName = exerciseName });
        }
    }
}

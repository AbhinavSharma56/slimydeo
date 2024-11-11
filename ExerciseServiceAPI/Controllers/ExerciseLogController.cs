using ExerciseServiceAPI.Models;
using ExerciseServiceAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExerciseServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseLogController : ControllerBase
    {
        private readonly IExerciseLogRepository _exerciseLogRepository;

        public ExerciseLogController(IExerciseLogRepository exerciseLogRepository)
        {
            _exerciseLogRepository = exerciseLogRepository;
        }

        // GET: api/exerciselogs
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var exerciseLogs = await _exerciseLogRepository.GetExerciseLogsAsync();
            return Ok(exerciseLogs);
        }

        // GET: api/exerciselog/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var exerciseLog = await _exerciseLogRepository.GetExerciseLogByIdAsync(id);
            if (exerciseLog == null)
            {
                return NotFound("Exercise Log not found.");
            }
            return Ok(exerciseLog);
        }

        // POST: api/exerciselog
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ExerciseLog exerciseLog)
        {
            if (exerciseLog == null)
            {
                return BadRequest("Exercise Log is null.");
            }

            await _exerciseLogRepository.InsertExerciseLogAsync(exerciseLog);
            return CreatedAtAction(nameof(Get), new { id = exerciseLog.LogId }, exerciseLog);
        }

        // PUT: api/exerciselog/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ExerciseLog exerciseLog)
        {
            if (exerciseLog == null || exerciseLog.LogId != id)
            {
                return BadRequest("Exercise Log ID mismatch.");
            }

            var existingExerciseLog = await _exerciseLogRepository.GetExerciseLogByIdAsync(id);
            if (existingExerciseLog == null)
            {
                return NotFound("Exercise Log not found.");
            }

            // Update the existing log's fields
            existingExerciseLog.UserId = exerciseLog.UserId;
            existingExerciseLog.ExerciseTypeId = exerciseLog.ExerciseTypeId;
            existingExerciseLog.Duration = exerciseLog.Duration;
            existingExerciseLog.CaloriesBurned = exerciseLog.CaloriesBurned;
            existingExerciseLog.ExerciseDate = exerciseLog.ExerciseDate;

            await _exerciseLogRepository.UpdateExerciseLogAsync(existingExerciseLog);
            return Ok("Exercise Log updated successfully.");
        }

        // DELETE: api/exerciselog/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exerciseLog = await _exerciseLogRepository.GetExerciseLogByIdAsync(id);
            if (exerciseLog == null)
            {
                return NotFound("Exercise Log not found.");
            }

            await _exerciseLogRepository.DeleteExerciseLogAsync(id);
            return Ok("Exercise Log deleted successfully.");
        }
    }
}

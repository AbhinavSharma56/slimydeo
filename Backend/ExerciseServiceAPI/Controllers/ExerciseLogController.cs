using ExerciseServiceAPI.Models;
using ExerciseServiceAPI.Repository;
using ExerciseServiceAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExerciseServiceAPI.Controllers
{
    [Route("api/ExerciseLog")]
    [ApiController]
    public class ExerciseLogController : ControllerBase
    {
        private readonly IExerciseLogRepository _exerciseLogRepository;
        private readonly IExerciseTypeRepository _exerciseTypeRepository;
        private readonly IExerciseService _exerciseService;

        public ExerciseLogController(IExerciseLogRepository exerciseLogRepository, IExerciseTypeRepository exerciseTypeRepository, IExerciseService exerciseService)
        {
            _exerciseLogRepository = exerciseLogRepository;
            _exerciseTypeRepository = exerciseTypeRepository;
            _exerciseService = exerciseService;
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

            // Get exercise name by ID
            var exerciseName = await _exerciseTypeRepository.GetExerciseNameByIdAsync(exerciseLog.ExerciseTypeId);
            if (exerciseName == null)
            {
                return BadRequest("Invalid Exercise Type ID.");
            }

            // Calculate calories burned using exercise name and duration
            var caloriesBurned = await _exerciseService.CalculateCaloriesBurnedAsync(exerciseName, exerciseLog.Duration);

            if (!caloriesBurned.HasValue)
                return BadRequest("Unable to calculate calories burned.");

            exerciseLog.CaloriesBurned = caloriesBurned.Value;

            // Insert exercise log
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
            existingExerciseLog.Username = exerciseLog.Username;
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

        // GET: api/exerciselog/total-calories/{userId}/7days
        [HttpGet("total-calories/{username}/7days")]
        public async Task<IActionResult> GetTotalCaloriesBurnedPerDay(string username)
        {
            var (success, message, data) = await _exerciseService.GetTotalCaloriesBurnedPerDayAsync(username);

            if (!success)
            {
                return NotFound(new { success, message });
            }

            return Ok(new { success, message, data });
        }

        // GET: api/exerciselog/user/{username}
        [HttpGet("user/{username}")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            try
            {
                var exerciseLogs = await _exerciseLogRepository.GetExerciseLogsByUsernameAsync(username);

                if (exerciseLogs == null || !exerciseLogs.Any())
                {
                    return NotFound(new { Message = "No exercise logs found for the given user." });
                }

                return Ok(exerciseLogs);
            }
            catch (Exception ex)
            {
                // Log the exception (using a logging framework)
                Console.Error.WriteLine($"Error in GetByUsername: {ex.Message}");
                return StatusCode(500, new { Message = "An internal server error occurred." });
            }
        }

        // GET: api/exerciselog/user/{username}/{date}
        [HttpGet("user/{username}/{date}")]
        public async Task<IActionResult> GetByUsernameAndDate(string username, DateTime date)
        {
            try
            {
                var exerciseLogs = await _exerciseLogRepository.GetExerciseLogsByUsernameAndDateAsync(username, date);

                if (exerciseLogs == null || !exerciseLogs.Any())
                {
                    return NotFound(new { Message = "No exercise logs found for the given user and date." });
                }

                return Ok(exerciseLogs);
            }
            catch (Exception ex)
            {
                // Log the exception (using a logging framework)
                Console.Error.WriteLine($"Error in GetByUsernameAndDate: {ex.Message}");
                return StatusCode(500, new { Message = "An internal server error occurred." });
            }
        }

    }
}

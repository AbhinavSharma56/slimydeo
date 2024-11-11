using MentalHealthServiceAPI.Models;
using MentalHealthServiceAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Transactions;

namespace MentalHealthServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoodController : ControllerBase
    {
        private readonly IMoodRepository _moodRepository;

        public MoodController(IMoodRepository moodRepository)
        {
            _moodRepository = moodRepository;
        }

        // GET: api/moods
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var moods = await _moodRepository.GetMoodsAsync();
            return Ok(moods);
        }

        // GET: api/mood/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var mood = await _moodRepository.GetMoodByIdAsync(id);
            if (mood == null)
            {
                return NotFound();
            }
            return Ok(mood);
        }

        // POST: api/mood
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Mood mood)
        {
            if (mood == null)
            {
                return BadRequest("Invalid mood data.");
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _moodRepository.InsertMoodAsync(mood);
                scope.Complete();
                return CreatedAtAction(nameof(GetById), new { id = mood.MoodId }, mood);
            }
        }

        // PUT: api/mood/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Mood mood)
        {
            if (mood == null || mood.MoodId != id)
            {
                return BadRequest("Mood ID mismatch.");
            }

            var existingMood = await _moodRepository.GetMoodByIdAsync(id);
            if (existingMood == null)
            {
                return NotFound("Mood not found.");
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _moodRepository.UpdateMoodAsync(mood);
                scope.Complete();
                return Ok("Mood updated successfully.");
            }
        }

        // DELETE: api/mood/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var mood = await _moodRepository.GetMoodByIdAsync(id);
            if (mood == null)
            {
                return NotFound("Mood not found.");
            }

            await _moodRepository.DeleteMoodAsync(id);
            return Ok("Mood deleted successfully.");
        }
    }
}

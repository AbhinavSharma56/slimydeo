using MentalHealthServiceAPI.Models;
using MentalHealthServiceAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MentalHealthServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentalHealthLogController : ControllerBase
    {
        private readonly IMentalHealthLogRepository _mentalHealthLogRepository;

        public MentalHealthLogController(IMentalHealthLogRepository mentalHealthLogRepository)
        {
            _mentalHealthLogRepository = mentalHealthLogRepository;
        }

        // GET: api/mentalhealthLogs
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var mentalHealthLogs = await _mentalHealthLogRepository.GetMentalHealthLogsAsync();
            return Ok(mentalHealthLogs);
        }

        // GET: api/mentalHealthLog/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var mentalHealthLog = await _mentalHealthLogRepository.GetMentalHealthLogByIdAsync(id);
            if (mentalHealthLog == null)
            {
                return NotFound("Mental Health Log not found.");
            }
            return Ok(mentalHealthLog);
        }

        // POST: api/mentalHealthLog
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MentalHealthLog mentalHealthLog)
        {
            if (mentalHealthLog == null)
            {
                return BadRequest("Mental Health Log is null.");
            }

            await _mentalHealthLogRepository.InsertMentalHealthLogAsync(mentalHealthLog);
            return CreatedAtAction(nameof(Get), new { id = mentalHealthLog.LogId }, mentalHealthLog);
        }

        // PUT: api/mentalHealthLog/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MentalHealthLog mentalHealthLog)
        {
            if (mentalHealthLog == null || mentalHealthLog.LogId != id)
            {
                return BadRequest("Mental Health Log ID mismatch.");
            }

            var existingMentalLog = await _mentalHealthLogRepository.GetMentalHealthLogByIdAsync(id);
            if (existingMentalLog == null)
            {
                return NotFound("Mental Health Log not found.");
            }

            // Update the existing log's fields
            existingMentalLog.UserId = mentalHealthLog.UserId;
            existingMentalLog.MoodId = mentalHealthLog.MoodId;
            existingMentalLog.Intensity = mentalHealthLog.Intensity;
            existingMentalLog.Notes = mentalHealthLog.Notes;
            existingMentalLog.LogDate = mentalHealthLog.LogDate;

            await _mentalHealthLogRepository.UpdateMentalHealthLogAsync(existingMentalLog);
            return Ok("Mental Health Log updated successfully.");
        }

        // DELETE: api/mentalHealthLog/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var mentalHealthLog = await _mentalHealthLogRepository.GetMentalHealthLogByIdAsync(id);
            if (mentalHealthLog == null)
            {
                return NotFound("Mental Health Log not found.");
            }

            await _mentalHealthLogRepository.DeleteMentalHealthLogAsync(id);
            return Ok("Mental Health Log deleted successfully.");
        }
    }
}

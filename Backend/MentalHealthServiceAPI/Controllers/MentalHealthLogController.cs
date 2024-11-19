using MentalHealthServiceAPI.Models;
using MentalHealthServiceAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MentalHealthServiceAPI.Controllers
{
    [Route("api/MentalHealthLog")]
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
                return BadRequest(new ResponseDto {
                    Success = false,
                    Message = "Mental Health Log is null."
                });
            }

            await _mentalHealthLogRepository.InsertMentalHealthLogAsync(mentalHealthLog);
            return Ok(new ResponseDto
            {
                Success = true,
                Message = "Added Mental health Log Successfully."
            });
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
            existingMentalLog.Username = mentalHealthLog.Username;
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
            // Check if the mental health log exists
            var mentalHealthLog = await _mentalHealthLogRepository.GetMentalHealthLogByIdAsync(id);
            if (mentalHealthLog == null)
            {
                return NotFound(new ResponseDto
                {
                    Success = false,
                    Message = "Mental Health Log not found."
                });
            }

            // Delete the log
            await _mentalHealthLogRepository.DeleteMentalHealthLogAsync(id);

            // Return success response
            return Ok(new ResponseDto
            {
                Success = true,
                Message = "Mental Health Log deleted successfully."
            });
        }


        // GET: api/mentalHealthLog/user/{username}
        [HttpGet("user/{username}")]
        public async Task<IActionResult> GetLogsByUser(string username)
        {
            var mentalHealthLog = await _mentalHealthLogRepository.GetMentalHealthLogByUsernameAsync(username);
            if (mentalHealthLog == null)
            {
                return NotFound("Mental Health Log not found.");
            }
            return Ok(mentalHealthLog);
        }

        public class ResponseDto
        {
            public bool Success { get; set; }
            public string Message { get; set; } = string.Empty;
        }

    }
}

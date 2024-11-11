using HealthMetricsServiceAPI.Models;
using HealthMetricsServiceAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthMetricsServiceAPI.Controllers
{
    [Route("api/HealthMetrics")]
    [ApiController]
    public class HealthMetricsController : ControllerBase
    {
        private readonly IHealthMetricsService _healthMetricsService;

        public HealthMetricsController(IHealthMetricsService healthMetricsService)
        {
            _healthMetricsService = healthMetricsService;
        }

        // GET: api/HealthMetrics/metrics
        [HttpGet("metrics")]
        public async Task<IActionResult> GetAllMetrics() =>
            Ok(await _healthMetricsService.GetAllMetricsAsync());

        // GET: api/HealthMetrics/metrics/{id}
        [HttpGet("metrics/{id}")]
        public async Task<IActionResult> GetMetricById(int id)
        {
            var metric = await _healthMetricsService.GetMetricByIdAsync(id);
            if (metric == null)
                return NotFound();
            return Ok(metric);
        }

        // POST: api/HealthMetrics/metrics
        [HttpPost("metrics")]
        public async Task<IActionResult> AddMetric([FromBody] Metric metric)
        {
            await _healthMetricsService.AddMetricAsync(metric);
            return CreatedAtAction(nameof(GetMetricById), new { id = metric.MetricId }, metric);
        }

        // PUT: api/HealthMetrics/metrics/{id}
        [HttpPut("metrics/{id}")]
        public async Task<IActionResult> UpdateMetric(int id, [FromBody] Metric metric)
        {
            if (id != metric.MetricId)
                return BadRequest();

            await _healthMetricsService.UpdateMetricAsync(metric);
            return NoContent();
        }

        // DELETE: api/HealthMetrics/metrics/{id}
        [HttpDelete("metrics/{id}")]
        public async Task<IActionResult> DeleteMetric(int id)
        {
            await _healthMetricsService.DeleteMetricAsync(id);
            return NoContent();
        }

        // GET: api/HealthMetrics/metrics/logs/{userid}
        [HttpGet("metrics/logs/{userId}")]
        public async Task<IActionResult> GetMetricsLogsByUserId(int userId) =>
            Ok(await _healthMetricsService.GetMetricsLogsByUserIdAsync(userId));

        // GET: api/HealthMetrics/metrics/logs/7days/{userid}
        [HttpGet("metrics/logs/7days/{userId}")]
        public async Task<IActionResult> GetMetricsLogsForPast7Days(int userId)
        {
            var logs = await _healthMetricsService.GetMetricsLogsForPast7DaysAsync(userId);
            if (logs == null || !logs.Any())
                return NotFound();

            return Ok(logs);
        }

        // GET: api/HealthMetrics/metrics/logs
        [HttpGet("metrics/logs")]
        public async Task<IActionResult> GetAllLogs()
        {
            var logs = await _healthMetricsService.GetAllLogsAsync();
            return Ok(logs);
        }

        // GET: api/HealthMetrics/metrics/log/{id}
        [HttpGet("metrics/log/{id}")]
        public async Task<IActionResult> GetLogById(int id)
        {
            var log = await _healthMetricsService.GetLogByIdAsync(id);
            if (log == null)
                return NotFound();
            return Ok(log);
        }

        // POST: api/HealthMetrics/metrics/logs
        [HttpPost("metrics/logs")]
        public async Task<IActionResult> AddLog([FromBody] MetricsLog log)
        {
            await _healthMetricsService.AddLogAsync(log);
            return CreatedAtAction(nameof(GetLogById), new { id = log.LogId }, log);
        }

        // PUT: api/HealthMetrics/metrics/logs/{id}
        [HttpPut("metrics/logs/{id}")]
        public async Task<IActionResult> UpdateLog(int id, [FromBody] MetricsLog log)
        {
            if (id != log.LogId)
                return BadRequest("Log ID mismatch");

            await _healthMetricsService.UpdateLogAsync(log);
            return NoContent();
        }

        // DELETE: api/HealthMetrics/metrics/logs/{id}
        [HttpDelete("metrics/logs/{id}")]
        public async Task<IActionResult> DeleteLog(int id)
        {
            await _healthMetricsService.DeleteLogAsync(id);
            return NoContent();
        }

        // GET: api/HealthMetrics/metrics/logs/last7/{userId}/{metricId}
        [HttpGet("metrics/logs/last7/{userId}/{metricId}")]
        public async Task<IActionResult> GetLast7Entries(int userId, int metricId)
        {
            var logs = await _healthMetricsService.GetLast7EntriesAsync(userId, metricId);
            if (logs == null || !logs.Any())
                return NotFound();

            return Ok(logs);
        }
    }
}

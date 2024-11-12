using HealthMetricsServiceAPI.Models;
using HealthMetricsServiceAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HealthMetricsServiceAPI.Controllers
{
    [Route("api/HealthMetrics")]
    [ApiController]
    public class HealthMetricsController : ControllerBase
    {
        private readonly IMetricRepository _metricRepository;
        private readonly IMetricLogRepository _metricLogRepository;

        public HealthMetricsController(IMetricRepository metricRepository, IMetricLogRepository metricLogRepository)
        {
            _metricRepository = metricRepository;
            _metricLogRepository = metricLogRepository;
        }

        [HttpGet("metrics")]
        public async Task<IActionResult> GetAllMetrics() =>
            Ok(await _metricRepository.GetAllMetricsAsync());

        [HttpGet("metrics/{id}")]
        public async Task<IActionResult> GetMetricById(int id)
        {
            var metric = await _metricRepository.GetMetricByIdAsync(id);
            return metric == null ? NotFound() : Ok(metric);
        }

        [HttpPost("metrics")]
        public async Task<IActionResult> AddMetric([FromBody] Metric metric)
        {
            bool success = await _metricRepository.AddMetricAsync(metric);
            return success ? CreatedAtAction(nameof(GetMetricById), new { id = metric.MetricId }, metric) : StatusCode(500, "Could not add metric.");
        }

        [HttpPut("metrics/{id}")]
        public async Task<IActionResult> UpdateMetric(int id, [FromBody] Metric metric)
        {
            if (id != metric.MetricId) return BadRequest("Metric ID mismatch");
            bool success = await _metricRepository.UpdateMetricAsync(metric);
            return success ? NoContent() : StatusCode(500, "Could not update metric.");
        }

        [HttpDelete("metrics/{id}")]
        public async Task<IActionResult> DeleteMetric(int id)
        {
            bool success = await _metricRepository.DeleteMetricAsync(id);
            return success ? NoContent() : StatusCode(500, "Could not delete metric.");
        }

        [HttpGet("metrics/logs/{username}")]
        public async Task<IActionResult> GetMetricsLogsByUserId(string username) =>
            Ok(await _metricLogRepository.GetLogsByUsernameAsync(username));

        [HttpGet("metrics/logs/7days/{username}")]
        public async Task<IActionResult> GetMetricsLogsForPast7Days(string username)
        {
            var logs = await _metricLogRepository.GetLogsForPast7DaysAsync(username);
            return logs == null || !logs.Any() ? NotFound() : Ok(logs);
        }

        [HttpGet("metrics/logs")]
        public async Task<IActionResult> GetAllLogs() =>
            Ok(await _metricLogRepository.GetAllLogsAsync());

        [HttpGet("metrics/log/{id}")]
        public async Task<IActionResult> GetLogById(int id)
        {
            var log = await _metricLogRepository.GetLogByIdAsync(id);
            return log == null ? NotFound() : Ok(log);
        }

        [HttpPost("metrics/logs")]
        public async Task<IActionResult> AddLog([FromBody] MetricsLog log)
        {
            bool success = await _metricLogRepository.AddLogAsync(log);
            return success ? CreatedAtAction(nameof(GetLogById), new { id = log.LogId }, log) : StatusCode(500, "Could not add log.");
        }

        [HttpPut("metrics/logs/{id}")]
        public async Task<IActionResult> UpdateLog(int id, [FromBody] MetricsLog log)
        {
            if (id != log.LogId) return BadRequest("Log ID mismatch");
            bool success = await _metricLogRepository.UpdateLogAsync(log);
            return success ? NoContent() : StatusCode(500, "Could not update log.");
        }

        [HttpDelete("metrics/logs/{id}")]
        public async Task<IActionResult> DeleteLog(int id)
        {
            bool success = await _metricLogRepository.DeleteLogAsync(id);
            return success ? NoContent() : StatusCode(500, "Could not delete log.");
        }

        [HttpGet("metrics/logs/last7/{username}/{metricId}")]
        public async Task<IActionResult> GetLast7Entries(string username, int metricId)
        {
            var logs = await _metricLogRepository.GetLast7EntriesAsync(username, metricId);
            return logs == null || !logs.Any() ? NotFound() : Ok(logs);
        }
    }
}
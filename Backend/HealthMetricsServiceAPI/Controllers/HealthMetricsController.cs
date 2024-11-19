using DietServiceAPI.Models;  // Ensure to include the ApiResponse model
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
        public async Task<ActionResult<ApiResponse<IEnumerable<Metric>>>> GetAllMetrics()
        {
            var metrics = await _metricRepository.GetAllMetricsAsync();
            return Ok(new ApiResponse<IEnumerable<Metric>>(metrics.Any(), metrics.Any() ? "Metrics retrieved successfully" : "No metrics found", metrics));
        }

        [HttpGet("metrics/{id}")]
        public async Task<ActionResult<ApiResponse<Metric>>> GetMetricById(int id)
        {
            var metric = await _metricRepository.GetMetricByIdAsync(id);
            return metric == null
                ? NotFound(new ApiResponse<Metric>(false, "Metric not found"))
                : Ok(new ApiResponse<Metric>(true, "Metric retrieved successfully", metric));
        }

        [HttpPost("metrics")]
        public async Task<ActionResult<ApiResponse<Metric>>> AddMetric([FromBody] Metric metric)
        {
            var addedMetric = await _metricRepository.AddMetricAsync(metric);
            if (addedMetric != false)
            {
                return Ok(new ApiResponse<Metric>(true, "Metric added successfully"));
            }
            return StatusCode(500, new ApiResponse<Metric>(false, "Could not add metric"));
        }

        [HttpPut("metrics/{id}")]
        public async Task<ActionResult<ApiResponse<Metric>>> UpdateMetric(int id, [FromBody] Metric metric)
        {
            if (id != metric.MetricId)
                return BadRequest(new ApiResponse<Metric>(false, "Metric ID mismatch"));

            var updatedMetric = await _metricRepository.UpdateMetricAsync(metric);
            return updatedMetric != false
                ? Ok(new ApiResponse<Metric>(true, "Metric updated successfully"))
                : StatusCode(500, new ApiResponse<Metric>(false, "Could not update metric"));
        }

        [HttpDelete("metrics/{id}")]
        public async Task<ActionResult<ApiResponse<Metric>>> DeleteMetric(int id)
        {
            var deletedMetric = await _metricRepository.DeleteMetricAsync(id);
            return deletedMetric != false
                ? Ok(new ApiResponse<Metric>(true, "Metric deleted successfully"))
                : StatusCode(500, new ApiResponse<Metric>(false, "Could not delete metric"));
        }

        [HttpGet("metrics/logs/{username}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<MetricsLog>>>> GetMetricsLogsByUserId(string username)
        {
            var logs = await _metricLogRepository.GetLogsByUsernameAsync(username);
            return Ok(new ApiResponse<IEnumerable<MetricsLog>>(logs.Any(), logs.Any() ? "Logs retrieved successfully" : "No logs found", logs));
        }

        [HttpGet("metrics/logs/7days/{username}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<MetricsLog>>>> GetMetricsLogsForPast7Days(string username)
        {
            var logs = await _metricLogRepository.GetLogsForPast7DaysAsync(username);
            return logs == null || !logs.Any()
                ? NotFound(new ApiResponse<IEnumerable<MetricsLog>>(false, "No logs found for the past 7 days"))
                : Ok(new ApiResponse<IEnumerable<MetricsLog>>(true, "Logs retrieved successfully", logs));
        }

        [HttpGet("metrics/logs")]
        public async Task<ActionResult<ApiResponse<IEnumerable<MetricsLog>>>> GetAllLogs()
        {
            var logs = await _metricLogRepository.GetAllLogsAsync();
            return Ok(new ApiResponse<IEnumerable<MetricsLog>>(logs.Any(), logs.Any() ? "Logs retrieved successfully" : "No logs found", logs));
        }

        [HttpGet("metrics/log/{id}")]
        public async Task<ActionResult<ApiResponse<MetricsLog>>> GetLogById(int id)
        {
            var log = await _metricLogRepository.GetLogByIdAsync(id);
            return log == null
                ? NotFound(new ApiResponse<MetricsLog>(false, "Log not found"))
                : Ok(new ApiResponse<MetricsLog>(true, "Log retrieved successfully", log));
        }

        [HttpPost("metrics/logs")]
        public async Task<ActionResult<ApiResponse<MetricsLog>>> AddLog([FromBody] MetricsLog log)
        {
            var addedLog = await _metricLogRepository.AddLogAsync(log);
            if (addedLog != false)
            {
                return Ok(new ApiResponse<MetricsLog>(true, "Log added successfully"));
            }
            return StatusCode(500, new ApiResponse<MetricsLog>(false, "Could not add log"));
        }

        [HttpPut("metrics/logs/{id}")]
        public async Task<ActionResult<ApiResponse<MetricsLog>>> UpdateLog(int id, [FromBody] MetricsLog log)
        {
            if (id != log.LogId)
                return BadRequest(new ApiResponse<MetricsLog>(false, "Log ID mismatch"));

            var updatedLog = await _metricLogRepository.UpdateLogAsync(log);
            return updatedLog != false
                ? Ok(new ApiResponse<MetricsLog>(true, "Log updated successfully"))
                : StatusCode(500, new ApiResponse<MetricsLog>(false, "Could not update log"));
        }

        [HttpDelete("metrics/logs/{id}")]
        public async Task<ActionResult<ApiResponse<MetricsLog>>> DeleteLog(int id)
        {
            var deletedLog = await _metricLogRepository.DeleteLogAsync(id);
            return deletedLog != false
                ? Ok(new ApiResponse<MetricsLog>(true, "Log deleted successfully"))
                : StatusCode(500, new ApiResponse<MetricsLog>(false, "Could not delete log"));
        }

        [HttpGet("metrics/logs/last7/{username}/{metricId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<MetricsLog>>>> GetLast7Entries(string username, int metricId)
        {
            var logs = await _metricLogRepository.GetLast7EntriesAsync(username, metricId);
            return logs == null || !logs.Any()
                ? NotFound(new ApiResponse<IEnumerable<MetricsLog>>(false, "No entries found for the last 7 days"))
                : Ok(new ApiResponse<IEnumerable<MetricsLog>>(true, "Entries retrieved successfully", logs));
        }
    }
}
using HealthMetricsServiceAPI.Models;
using HealthMetricsServiceAPI.Repositories;

namespace HealthMetricsServiceAPI.Services
{
    public class HealthMetricsService : IHealthMetricsService
    {
        private readonly IMetricRepository _metricRepository;
        private readonly IMetricLogRepository _metricLogRepository;

        public HealthMetricsService(IMetricRepository metricRepository, IMetricLogRepository metricLogRepository)
        {
            _metricRepository = metricRepository;
            _metricLogRepository = metricLogRepository;
        }

        public async Task<IEnumerable<Metric>> GetAllMetricsAsync() =>
            await _metricRepository.GetAllMetricsAsync();

        public async Task<Metric> GetMetricByIdAsync(int id) =>
            await _metricRepository.GetMetricByIdAsync(id);

        public async Task AddMetricAsync(Metric metric) =>
            await _metricRepository.AddMetricAsync(metric);

        public async Task UpdateMetricAsync(Metric metric) =>
            await _metricRepository.UpdateMetricAsync(metric);

        public async Task DeleteMetricAsync(int id) =>
            await _metricRepository.DeleteMetricAsync(id);

        public async Task<IEnumerable<MetricsLog>> GetMetricsLogsByUserIdAsync(int userId) =>
            await _metricLogRepository.GetLogsByUserIdAsync(userId);

        public async Task<IEnumerable<MetricsLog>> GetMetricsLogsForPast7DaysAsync(int userId) =>
            await _metricLogRepository.GetLogsForPast7DaysAsync(userId);

        public async Task<IEnumerable<MetricsLog>> GetAllLogsAsync() =>
            await _metricLogRepository.GetAllLogsAsync();

        public async Task<MetricsLog> GetLogByIdAsync(int id) =>
            await _metricLogRepository.GetLogByIdAsync(id);

        public async Task AddLogAsync(MetricsLog log) =>
            await _metricLogRepository.AddLogAsync(log);

        public async Task UpdateLogAsync(MetricsLog log) =>
            await _metricLogRepository.UpdateLogAsync(log);

        public async Task DeleteLogAsync(int id) =>
            await _metricLogRepository.DeleteLogAsync(id);
        public async Task<IEnumerable<MetricsLog>> GetLast7EntriesAsync(int userId, int metricId) =>
            await _metricLogRepository.GetLast7EntriesAsync(userId, metricId);
    }
}

using HealthMetricsServiceAPI.Models;

namespace HealthMetricsServiceAPI.Services
{
    public interface IHealthMetricsService
    {
        Task<IEnumerable<Metric>> GetAllMetricsAsync();
        Task<Metric> GetMetricByIdAsync(int id);
        Task AddMetricAsync(Metric metric);
        Task UpdateMetricAsync(Metric metric);
        Task DeleteMetricAsync(int id);
        Task<IEnumerable<MetricsLog>> GetMetricsLogsByUserIdAsync(int userId);
        Task<IEnumerable<MetricsLog>> GetMetricsLogsForPast7DaysAsync(int userId);
        Task<IEnumerable<MetricsLog>> GetAllLogsAsync();
        Task<MetricsLog> GetLogByIdAsync(int id);
        Task AddLogAsync(MetricsLog log);
        Task UpdateLogAsync(MetricsLog log);
        Task DeleteLogAsync(int id); 
        Task<IEnumerable<MetricsLog>> GetLast7EntriesAsync(int userId, int metricId);
    }
}

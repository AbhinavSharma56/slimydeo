using HealthMetricsServiceAPI.Models;

namespace HealthMetricsServiceAPI.Repositories
{
    public interface IMetricLogRepository
    {
        Task<IEnumerable<MetricsLog>> GetAllLogsAsync();
        Task<MetricsLog?> GetLogByIdAsync(int id);

        // To get all logs of a particular user.
        Task<IEnumerable<MetricsLog>> GetLogsByUserIdAsync(int userId);

        // To get all logs of user in past 7 days.
        Task<IEnumerable<MetricsLog>> GetLogsForPast7DaysAsync(int userId);

        // To get the last 7 entries of a given metric of a given user.
        Task<IEnumerable<MetricsLog>> GetLast7EntriesAsync(int userId, int metricId);
        Task<bool> AddLogAsync(MetricsLog log);
        Task<bool> UpdateLogAsync(MetricsLog log);
        Task<bool> DeleteLogAsync(int id);
    }
}

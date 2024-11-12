using HealthMetricsServiceAPI.Models;

namespace HealthMetricsServiceAPI.Repositories
{
    public interface IMetricRepository
    {
        Task<IEnumerable<Metric>> GetAllMetricsAsync();
        Task<Metric?> GetMetricByIdAsync(int id);
        Task<bool> AddMetricAsync(Metric metric);
        Task<bool> UpdateMetricAsync(Metric metric);
        Task<bool> DeleteMetricAsync(int id);
    }
}
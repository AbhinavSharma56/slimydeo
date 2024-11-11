using HealthMetricsServiceAPI.Models;

namespace HealthMetricsServiceAPI.Repositories
{
    public interface IMetricRepository
    {
        Task<IEnumerable<Metric>> GetAllMetricsAsync();
        Task<Metric> GetMetricByIdAsync(int id);
        Task AddMetricAsync(Metric metric);
        Task UpdateMetricAsync(Metric metric);
        Task DeleteMetricAsync(int id);
    }
}

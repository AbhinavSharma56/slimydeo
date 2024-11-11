using HealthMetricsServiceAPI.Data;
using HealthMetricsServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthMetricsServiceAPI.Repositories
{
    public class MetricRepository : IMetricRepository
    {
        private readonly HealthMetricsDbContext _context;

        public MetricRepository(HealthMetricsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Metric>> GetAllMetricsAsync() =>
            await _context.Metrics.ToListAsync();

        public async Task<Metric> GetMetricByIdAsync(int id) =>
            await _context.Metrics.FindAsync(id);

        public async Task AddMetricAsync(Metric metric)
        {
            _context.Metrics.Add(metric);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMetricAsync(Metric metric)
        {
            _context.Metrics.Update(metric);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMetricAsync(int id)
        {
            var metric = await GetMetricByIdAsync(id);
            if (metric != null)
            {
                _context.Metrics.Remove(metric);
                await _context.SaveChangesAsync();
            }
        }
    }
}

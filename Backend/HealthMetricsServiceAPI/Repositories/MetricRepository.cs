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

        public async Task<Metric?> GetMetricByIdAsync(int id) =>
            await _context.Metrics.FindAsync(id);

        public async Task<bool> AddMetricAsync(Metric metric)
        {
            _context.Metrics.Add(metric);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateMetricAsync(Metric metric)
        {
            _context.Metrics.Update(metric);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteMetricAsync(int id)
        {
            var metric = await GetMetricByIdAsync(id);
            if (metric == null) return false;

            _context.Metrics.Remove(metric);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
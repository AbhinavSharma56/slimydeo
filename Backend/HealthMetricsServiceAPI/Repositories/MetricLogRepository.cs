using HealthMetricsServiceAPI.Data;
using HealthMetricsServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthMetricsServiceAPI.Repositories
{
    public class MetricLogRepository : IMetricLogRepository
    {
        private readonly HealthMetricsDbContext _context;

        public MetricLogRepository(HealthMetricsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MetricsLog>> GetAllLogsAsync() =>
            await _context.MetricsLogs.ToListAsync();

        public async Task<MetricsLog> GetLogByIdAsync(int id) =>
            await _context.MetricsLogs.FindAsync(id);

        public async Task<IEnumerable<MetricsLog>> GetLogsByUserIdAsync(int userId) =>
            await _context.MetricsLogs.Where(log => log.UserId == userId).ToListAsync();

        public async Task<IEnumerable<MetricsLog>> GetLogsByMetricIdAsync(int metricId) =>
            await _context.MetricsLogs.Where(log => log.MetricId == metricId).ToListAsync();

        public async Task AddLogAsync(MetricsLog log)
        {
            _context.MetricsLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLogAsync(MetricsLog log)
        {
            _context.MetricsLogs.Update(log);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLogAsync(int id)
        {
            var log = await GetLogByIdAsync(id);
            if (log != null)
            {
                _context.MetricsLogs.Remove(log);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<MetricsLog>> GetLogsForPast7DaysAsync(int userId)
        {
            DateTime sevenDaysAgo = DateTime.Now.AddDays(-7);
            return await _context.MetricsLogs
                                 .Where(log => log.UserId == userId && log.DateRecorded >= sevenDaysAgo)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<MetricsLog>> GetLast7EntriesAsync(int userId, int metricId)
        {
            return await _context.MetricsLogs
                .Where(log => log.UserId == userId && log.MetricId == metricId)
                .OrderByDescending(log => log.DateRecorded)
                .Take(7)
                .ToListAsync();
        }
    }
}

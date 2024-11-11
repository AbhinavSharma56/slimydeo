using HealthMetricsServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthMetricsServiceAPI.Data
{
    public class HealthMetricsDbContext : DbContext
    {
        public DbSet<Metric> Metrics { get; set; }
        public DbSet<MetricsLog> MetricsLogs { get; set; }

        public HealthMetricsDbContext(DbContextOptions<HealthMetricsDbContext> options) : base(options) { }
    }
}

using System.ComponentModel.DataAnnotations;

namespace HealthMetricsServiceAPI.Models
{
    public class MetricsLog
    {
        [Key]
        public int LogId { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public int MetricId { get; set; }  // Foreign key to the Metric model
        [Required]
        public float Value { get; set; }
        [Required]
        public DateTime DateRecorded { get; set; }
    }
}

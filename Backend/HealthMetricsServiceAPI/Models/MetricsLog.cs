using System.ComponentModel.DataAnnotations;

namespace HealthMetricsServiceAPI.Models
{
    public class MetricsLog
    {
        [Key]
        public int LogId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int MetricId { get; set; }  // Foreign key to the Metric model
        [Required]
        public float Value { get; set; }
        [Required]
        public DateTime DateRecorded { get; set; }

        // Navigation property to the Metric model
        public Metric? Metric { get; set; }

    }
}
